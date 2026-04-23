using UnityEngine;

public class BerserkState : IState
{
    public MonsterState MonsterState => MonsterState.Berserk;
    private AIComponent ai;

    public BerserkState(AIComponent ai)
    {
        this.ai = ai;
    }

    public void Enter()
    {
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
