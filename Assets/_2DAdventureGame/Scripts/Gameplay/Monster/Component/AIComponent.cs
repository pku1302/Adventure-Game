using UnityEngine;
using System;

public abstract class AIComponent : MonoBehaviour
{
    public MovementComponent Movement { get; private set; }
    public AnimationComponent Animation { get; private set; }
    public HealthComponent Health { get; private set; }
    public HitComponent Hit { get; private set; }
    public LootComponent Loot { get; private set; }
    [HideInInspector]
    public LootTable lootTable;
    public Monster Monster { get; protected set; }
    protected StateMachine stateMachine;

    protected IState chaseState;
    protected IState stopState;
    protected IState deadState;
    protected IState wanderState;
    protected IState hitState;


    protected void Init()
    {
        Movement = GetComponent<MovementComponent>();
        Animation = GetComponent<AnimationComponent>();
        Health = GetComponent<HealthComponent>();
        Hit = GetComponent<HitComponent>();
        Loot = GetComponent<LootComponent>();

        chaseState = new ChaseState(this);
        stopState = new StopState(this);
        deadState = new DeadState(this);
        wanderState = new WanderState(this);
        hitState = new HitState(this);
        stateMachine = new StateMachine();

        if (Health != null)
        {
            Health.OnDeath += HandleDeath;
        }
    }

    public void ChangeState(IState newState)
    {
        stateMachine.ChangeState(newState);
    }

    public MonsterState GetState()
    {
        return stateMachine.CurrentState.MonsterState;
    }

    private void HandleDeath()
    {
        stateMachine.ChangeState(new DeadState(this));
        LootComponent loot = GetComponent<LootComponent>();
        loot.lootItems = lootTable.GenerateLoot();
    }

    public bool IsPlayerInAttackRange()
    {
        float playerDistance = Vector2.Distance(transform.position, PlayerController.player.transform.position);

        return playerDistance <= Monster.Data.attackRange;
    }
}
