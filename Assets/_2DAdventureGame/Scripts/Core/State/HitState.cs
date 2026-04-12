using UnityEngine;

public class HitState : IState
{
    public MonsterState MonsterState => MonsterState.Hit;

    private AIComponent ai;
    private Vector2 direction;

    public HitState(AIComponent ai, Vector2 direction)
    {
        this.ai = ai;
        this.direction = direction;
    }

    public void Enter()
    {
        ai.Animation.SetHit(direction);
        ai.Hit.TakeHit();
    }

    public void Exit()
    {
    }

    public void FixedUpdate()
    {
    }

    public void Update()
    {
        ai.Movement.StopMonster();
    }
}
