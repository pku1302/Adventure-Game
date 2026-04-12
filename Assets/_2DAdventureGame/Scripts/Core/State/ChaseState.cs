using UnityEngine;

public class ChaseState : IState
{
    public MonsterState MonsterState => MonsterState.Chase;

    private AIComponent ai;
    private Vector2 direction;
    private Vector2 playerRandomPosition;
    private float locateTimer = 0f;
    private float locateTime = 1f;
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

    }

    public void Exit()
    {
    }

    public void FixedUpdate()
    {
        float distance = Vector2.Distance(ai.transform.position, PlayerController.player.transform.position);
        direction = (playerRandomPosition - (Vector2)ai.transform.position).normalized;

        ai.Movement.Move(playerRandomPosition);
        ai.Animation.SetMove(direction);
    }

    public void Update()
    {
        if (locateTimer <= 0f)
        {
            playerRandomPosition = (Vector2)PlayerController.player.transform.position + Random.insideUnitCircle * attackRange;
            locateTimer = locateTime;
        }
        locateTimer -= Time.deltaTime;
    }
}
