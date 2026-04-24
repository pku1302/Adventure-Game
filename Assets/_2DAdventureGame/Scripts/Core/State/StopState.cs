using UnityEngine;

public class StopState : IState
{
    public MonsterState MonsterState => MonsterState.Stop;
    private AIComponent ai;
    private Vector2 direction;
    private float directionTimer;

    public StopState(AIComponent ai)
    {
        this.ai = ai;
    }

    public void Enter()
    {
        direction = (ai.target.position - ai.transform.position).normalized;
        ai.Movement.StopMonster();
        ai.Animation.SetStop(direction);
        directionTimer = 1f;
    }

    public void Exit()
    {
    }

    public void FixedUpdate()
    {
    }

    public void Update()
    {
        directionTimer -= Time.deltaTime;
        if (directionTimer <= 0f)
        {
            direction = (ai.target.position - ai.transform.position).normalized;
            ai.Animation.SetStop(direction);
        }
    }
}
