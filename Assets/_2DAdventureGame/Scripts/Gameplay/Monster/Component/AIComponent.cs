using UnityEngine;
using System;

public abstract class AIComponent : MonoBehaviour
{
    public MovementComponent Movement { get; private set; }
    public AnimationComponent Animation { get; private set; }
    public HealthComponent Health { get; private set; }
    public HitComponent Hit { get; private set; }
    public LootComponent Loot { get; private set; }
    public event Action OnAttackEnd;
    public LootTable lootTable;
    public Monster Monster { get; protected set; }
    protected StateMachine stateMachine;

    public void OnAttackEndEvent()
    {
        OnAttackEnd?.Invoke();
    }

    protected void Init()
    {
        Movement = GetComponent<MovementComponent>();
        Animation = GetComponent<AnimationComponent>();
        Health = GetComponent<HealthComponent>();
        Hit = GetComponent<HitComponent>();
        Loot = GetComponent<LootComponent>();

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
