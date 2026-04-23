using System;
using System.Net.NetworkInformation;
using UnityEngine;

public class JiangshiAIComponent : AIComponent
{
    private float stateChangeTimer;
    private int currentAngry = 0;
    private int currentStop = 0;
    private AttackState attackState;
    private GuardState guardState;
    private float guardTimer = 0f;

    public const int AngryGage = 150;
    public const int StopGage = 1;
    public int guardStack = 1;
    public event Action OnGuard;

    public JiangshiAttackComponent Attack { get; private set; }

    void Awake()
    {
        Monster = GetComponent<Monster>();
        Attack = GetComponent<JiangshiAttackComponent>();
        Init();
        attackState = new AttackState(this, Attack);
        guardState = new GuardState(this);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateMachine.Initialize(wanderState);
        Health.OnHit += HandleHit;
        Health.OnGuard += HandleGuard;
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
        if (guardTimer <= 0f)
        {
            guardStack = Mathf.Clamp(guardStack + 1, 0, 2);
            guardTimer = 6f;
        }
        stateChangeTimer -= Time.deltaTime;
        guardTimer -= Time.deltaTime;
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
        stateChangeTimer = Monster.Data.attackSpeed / 8;
        currentAngry = AngryGage;
    }

    void HandleGuard()
    {
        stateMachine.ChangeState(guardState);
        stateChangeTimer = Monster.Data.attackSpeed / 8;
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
