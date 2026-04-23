using UnityEngine;

public class HitState : IState
{
    public MonsterState MonsterState => MonsterState.Hit;

    private AIComponent ai;

    public HitState(AIComponent ai)
    {
        this.ai = ai;
    }

    public void Enter()
    {
        ai.Animation.SetHit();
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
