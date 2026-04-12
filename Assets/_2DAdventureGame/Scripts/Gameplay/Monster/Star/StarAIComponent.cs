using UnityEngine;
using System;

public class StarAIComponent : AIComponent
{
    private float stateChangeTimer;
    private int currentAngry = 0;
    private int currentStop = 0;
    public const int AngryGage = 10;
    public const int StopGage = 1;

    public AttackComponent Attack { get; private set; }

    public void OnHitEnd()
    {
        if (stateMachine.CurrentState.MonsterState == MonsterState.Dead)
            return;

        stateMachine.ChangeState(new ChaseState(this));
    }

    void Awake()
    {
        Init();
        Health.OnHit += HandleHit;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Monster = GetComponent<Monster>();
        Attack = GetComponent<AttackComponent>();
        stateMachine.Initialize(new WanderState(this));
        stateChangeTimer = Monster.Data.attackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        if (stateChangeTimer <= 0f)
        {
            Think();
            stateChangeTimer = Monster.Data.attackSpeed;
        }
        stateChangeTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    void HandleHit(Vector2 direction)
    {
        stateMachine.ChangeState(new HitState(this, direction));
        currentAngry = AngryGage;
    }

    void Think()
    {
        if (TryDead()) return;
        if (TryAttack()) return;
        if (TryStop()) return;
        if (TryChase()) return;

        TryWander();
    }

    bool TryDead()
    {
        if (stateMachine.CurrentState.MonsterState == MonsterState.Dead)
        {
            Health.OnHit -= HandleHit;
            return true;
        }

        return false;
    }

    bool TryAttack()
    {
        if (IsPlayerInAttackRange())
        {
            stateMachine.ChangeState(new AttackState(this, Attack));
            return true;
        }

        return false;
    }

    bool TryStop()
    {
        if (currentStop <= 0)
        {
            return false;
        }
        else
        {
            stateMachine.ChangeState(new StopState(this));
            currentStop -= 1;
            return true;
        }
    }

    bool TryChase()
    {
        float playerDistance = Vector2.Distance(transform.position, PlayerController.player.transform.position);
        if (playerDistance <= Monster.Data.detectRange)
        {
            stateMachine.ChangeState(new ChaseState(this));
            currentAngry = AngryGage;
            return true;
        }

        // 거리가 감지 밖이고 화난 상태라면
        if (currentAngry > 0)
        {
            stateMachine.ChangeState(new ChaseState(this));
            currentAngry -= 1;

            if (currentAngry == 0)
            {
                stateMachine.ChangeState(new StopState(this));
                currentStop = StopGage;
            }
            return true;
        }

        return false;
    }

    bool TryWander()
    {
        stateMachine.ChangeState(new WanderState(this));
        return true;
    }
}
