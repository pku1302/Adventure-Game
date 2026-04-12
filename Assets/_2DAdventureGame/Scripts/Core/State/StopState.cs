using UnityEngine;

public class StopState : IState
{
    public MonsterState MonsterState => MonsterState.Stop;
    private AIComponent ai;
    private Vector2 direction;

    public StopState(AIComponent ai)
    {
        this.ai = ai;
    }

    public void Enter()
    {
        direction = (PlayerController.player.transform.position - ai.transform.position).normalized;
        ai.Movement.StopMonster();
        ai.Animation.SetStop(direction);
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
