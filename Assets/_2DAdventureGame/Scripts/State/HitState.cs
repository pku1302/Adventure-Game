using UnityEngine;

public class HitState : IState
{
    private AttackMonsterAIComponent ai;
    private int damage;
    private Vector2 direction;


    public HitState(AttackMonsterAIComponent ai, int damage, Vector2 direction)
    {
        this.ai = ai;
        this.damage = damage;
        this.direction = direction;
    }

    public void Enter()
    {
        ai.Hit.TakeHit(damage, direction);
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
        ai.Movement.StopMonster();

        if(state.normalizedTime >= 1f)
        {
            ai.ChangeState(new ChaseState(ai));
        }

    }
}
