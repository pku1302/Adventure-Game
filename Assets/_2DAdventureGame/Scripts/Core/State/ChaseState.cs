using UnityEngine;

public class ChaseState : IState
{
    public MonsterState MonsterState => MonsterState.Chase;

    private AIComponent ai;
    private Vector2 direction;
    private Vector2 playerRandomPosition;
    private float animationTimer = 0f;
    private float AnimateTime = 0.2f;
    private float detectRange;
    private float attackRange;
    public ChaseState(AIComponent ai)
    {
        this.ai = ai;
        detectRange = ai.Monster.Data.detectRange;
        attackRange = ai.Monster.Data.attackRange;
    }
    public void Enter()
    {
        direction = (ai.target.position - ai.transform.position).normalized;
        playerRandomPosition = (Vector2)ai.target.position;
        ai.Animation.SetMove(direction);
        animationTimer = AnimateTime;
    }

    public void Exit()
    {
    }

    public void FixedUpdate()
    {

    }

    public void Update()
    {
        if (animationTimer <= 0f)
        {
            direction = (playerRandomPosition - (Vector2)ai.transform.position).normalized;
            playerRandomPosition = (Vector2)ai.target.position;
            animationTimer = AnimateTime;
            ai.Animation.SetMove(direction);
        }
        else
        {
            animationTimer -= Time.deltaTime;
        }
        //playerRandomPosition = (Vector2)PlayerController.player.transform.position + Random.insideUnitCircle * attackRange;
        ai.Movement.Move(playerRandomPosition);
    }
}
