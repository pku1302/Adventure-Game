using UnityEngine;

public class AttackState : IState
{
    public MonsterState MonsterState => MonsterState.Attack;
    private float attackCoolTime;

    private AIComponent ai;
    private AttackComponent attackComponent;
    private Vector2 direction;

    public AttackState(AIComponent ai, AttackComponent attackComponent)
    {
        this.ai = ai;
        this.attackComponent = attackComponent;
    }

    public void Enter()
    {
        attackCoolTime = 0f;
    }

    public void Exit()
    {
    }

    void Attack()
    {
        direction = (PlayerController.player.transform.position - ai.transform.position).normalized;
        ai.Animation.SetAttack(direction);
        attackComponent.direction = direction;
    }

    public void FixedUpdate()
    {
    }

    public void Update()
    {
        ai.Movement.StopMonster();

        if (attackCoolTime <= 0f && ai.IsPlayerInAttackRange())
        {
            Attack();
            attackCoolTime = ai.Monster.Data.attackSpeed;
        }
        else
        {
            attackCoolTime -= Time.deltaTime;
            ai.Animation.SetStop(direction);
        }
    }
}
