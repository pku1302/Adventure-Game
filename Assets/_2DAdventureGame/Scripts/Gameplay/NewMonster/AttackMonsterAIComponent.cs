using UnityEngine;
using System;

public class AttackMonsterAIComponent : AIComponent
{
    private float stateChangeTimer;
    public float detectRange = 5f;
    public AttackComponent Attack;
    public event Action OnAttackEnd;
    public Monster Monster;

    public void OnAttackEndEvent()
    {
        OnAttackEnd?.Invoke();
    }

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
        stateMachine.Initialize(new WanderState(this));
        Attack = GetComponent<AttackComponent>();
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
    }

    void Think()
    {
        if (TryDead()) return;
        if (TryAttack()) return;
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
        if(IsPlayerInAttackRange())
        {
            stateMachine.ChangeState(new AttackState(this));
            return true;
        }

        return false;
    }

    bool TryChase()
    {
        float playerDistance = Vector2.Distance(transform.position, PlayerController.player.transform.position);
        if (playerDistance <= detectRange)
        {
            stateMachine.ChangeState(new ChaseState(this));
            return true;
        }
        return false;
    }

    bool TryWander()
    {
        stateMachine.ChangeState(new WanderState(this));
        return true;
    }

    public bool IsPlayerInAttackRange()
    {
        float playerDistance = Vector2.Distance(transform.position, PlayerController.player.transform.position);

        return playerDistance <= Monster.Data.attackRange;
    }
}
