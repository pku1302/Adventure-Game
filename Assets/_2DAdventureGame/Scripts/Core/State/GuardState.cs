using UnityEngine;

public class GuardState : IState
{
    public MonsterState MonsterState => MonsterState.Guard;
    private AIComponent ai;
    private Vector2 dir;

    public GuardState(AIComponent ai)
    {
        this.ai = ai;
    }

    public void Enter()
    {
        dir = (PlayerController.player.transform.position - ai.transform.position).normalized;
        ai.Animation.SetGuard(dir);
       
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
