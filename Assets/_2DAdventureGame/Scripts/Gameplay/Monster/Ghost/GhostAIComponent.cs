using UnityEngine;

public class GhostAIComponent : AIComponent
{
    private float stateChangeTimer;
    private AttackState attackState;

    public GhostAttackComponent Attack { get; private set; }

    private void Awake()
    {
        Monster = GetComponent<Monster>();
        Attack = GetComponent<GhostAttackComponent>();
        Init();
        attackState = new AttackState(this, Attack);
    }

    void HandleHit(bool isCritical)
    {
        Hit.TakeHit(isCritical);
        if (!Attack.isAttacking)
        {
            stateMachine.ChangeState(hitState);
        }
    }

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

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
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
        if (stateMachine.CurrentState.MonsterState == MonsterState.Chase && target != null)
        {
            stateMachine.ChangeState(stopState);
            return true;
        }
        else
        {
            return false;
        }
    }

    bool TryChase()
    {
        if (IsPlayerInDetectRange())
        {
            stateMachine.ChangeState(chaseState);
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
