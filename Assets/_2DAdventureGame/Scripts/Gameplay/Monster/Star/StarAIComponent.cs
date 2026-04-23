using UnityEngine;
using System;

public class StarAIComponent : AIComponent
{
    private float stateChangeTimer;
    private int currentAngry = 0;
    private int currentStop = 0;
    private AttackState attackState;

    public const int AngryGage = 10;
    public const int StopGage = 1;

    public StarAttackComponent Attack { get; private set; }

    void Awake()
    {
        Monster = GetComponent<Monster>();
        Attack = GetComponent<StarAttackComponent>();
        Init();
        attackState = new AttackState(this, Attack);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateMachine.Initialize(wanderState);
        Health.OnHit += HandleHit;
        stateChangeTimer = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        if (!Attack.isAttacking && stateChangeTimer <= 0f)
        {
            stateChangeTimer = 0.1f;
            Think();
        }
        stateChangeTimer -= Time.deltaTime;
    }

    public void OnHitEnd()
    {
        if (stateMachine.CurrentState.MonsterState == MonsterState.Dead)
            return;
        stateMachine.ChangeState(stopState);
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }


    void HandleHit()
    {
        stateMachine.ChangeState(hitState);
        stateChangeTimer = Monster.Data.attackSpeed / 4;
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
            stateMachine.ChangeState(attackState);
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
            stateMachine.ChangeState(stopState);
            currentStop -= 1;
            return true;
        }
    }

    bool TryChase()
    {
        float playerDistance = Vector2.Distance(transform.position, PlayerController.player.transform.position);
        if (playerDistance <= Monster.Data.detectRange)
        {
            stateMachine.ChangeState(chaseState);
            currentAngry = AngryGage;
            return true;
        }

        // 거리가 감지 밖이고 화난 상태라면
        if (currentAngry > 0)
        {
            stateMachine.ChangeState(chaseState);
            currentAngry -= 1;

            if (currentAngry == 0)
            {
                stateMachine.ChangeState(stopState);
                currentStop = StopGage;
            }
            return true;
        }

        return false;
    }

    bool TryWander()
    {
        stateMachine.ChangeState(wanderState);
        return true;
    }
}
