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
    public Transform target;
    [SerializeField]
    protected DetectionComponent Detection;

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

        Detection.OnTargetEnter += t => target = t;
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
        Loot.InitializeLootItems(lootTable.GenerateLoot());
    }

    public bool IsPlayerInAttackRange()
    {
        if (target == null)
        {
            return false;
        }

        float distance = Vector2.Distance(target.position, transform.position);

        return  distance <= Monster.Data.attackRange;
    }

    public bool IsPlayerInDetectRange()
    {
        if (target == null)
        {
            return false;
        }

        float distance = Vector2.Distance(target.position, transform.position);

        return distance <= Monster.Data.detectRange;
    }
}
