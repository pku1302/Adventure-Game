using UnityEngine;

public class AttackState : IState
{
    private AttackMonsterAIComponent ai;
    private float attackDelay = 2f;
    private float attackDelayTimer = 0f;
    private Vector2 direction;

    public AttackState(AttackMonsterAIComponent ai)
    {
        this.ai = ai;
    }

    public void Enter()
    {
        direction = (PlayerController.player.transform.position - ai.transform.position).normalized;
        ai.Animation.SetAttack(direction);
        ai.Movement.StopMonster();
        ai.Attack.direction = direction;
    }

    public void Exit()
    {
    }

    public void FixedUpdate()
    {
    }

    public void Update()
    {
        AnimatorStateInfo state = ai.Animation.animator.GetCurrentAnimatorStateInfo(0);
        if (state.IsName("Attack") && state.normalizedTime >= 1f)
        {
            ai.Animation.SetStop(direction);
        }

        attackDelayTimer += Time.deltaTime;

        if (attackDelayTimer >= attackDelay)
        {
            ai.ChangeState(new ChaseState(ai));
        }
    }
}
