using UnityEngine;

public class DeadState : IState
{
    public MonsterState MonsterState => MonsterState.Dead;

    private AIComponent ai;

    public DeadState(AIComponent ai)
    {
        this.ai = ai;
    }

    public void Enter()
    {
        ai.Movement.StopMonster();
        ai.Animation.SetDead();
    }

    public void Exit()
    {
    }

    public void FixedUpdate()
    {
    }

    public void Update()
    {
    }
}
