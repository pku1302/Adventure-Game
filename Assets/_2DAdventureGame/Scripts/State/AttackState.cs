using UnityEngine;

public class AttackState : IState
{
    public MonsterState MonsterState => MonsterState.Attack;
    private float attackCoolTime;

    private AttackMonsterAIComponent ai;
    private Vector2 direction;
    private bool canAttack = true;

    public AttackState(AttackMonsterAIComponent ai)
    {
        this.ai = ai;
    }

    public void Enter()
    {
        Attack();
        ai.OnAttackEnd += HandleAttackEnd;
        attackCoolTime = ai.Monster.Data.attackSpeed;
    }

    public void Exit()
    {
        ai.OnAttackEnd -= HandleAttackEnd;
    }

    void HandleAttackEnd()
    {
        canAttack = false;
    }

    void Attack()
    {
        direction = (PlayerController.player.transform.position - ai.transform.position).normalized;
        ai.Animation.SetAttack(direction);
        ai.Attack.direction = direction;
    }

    public void FixedUpdate()
    {
        ai.Movement.StopMonster();
        if(!canAttack)
        {
            ai.Animation.SetStop(direction);
        }
    }

    public void Update()
    {
        if (!canAttack)
        {
            attackCoolTime -= Time.deltaTime;
        }

        if (attackCoolTime <= 0f && ai.IsPlayerInAttackRange())
        {
            Attack();
            attackCoolTime = ai.Monster.Data.attackSpeed;
            canAttack = true;
        }
    }
}
