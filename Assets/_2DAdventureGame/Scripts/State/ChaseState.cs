using UnityEngine;

public class ChaseState : IState
{
    public MonsterState MonsterState => MonsterState.Chase;

    private AttackMonsterAIComponent ai;
    private float attackRange = 1.5f;
    private float detectRange = 5f;
    private Vector2 direction;

    private Vector2 playerRandomPosition;
    private float locateTimer = 0f;
    private float locateTime = 1f;

    public ChaseState(AttackMonsterAIComponent ai)
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
        float distance = Vector2.Distance(ai.transform.position, PlayerController.player.transform.position);
        direction = (playerRandomPosition - (Vector2)ai.transform.position).normalized; 

        // 인식 범위에서 벗어났을 때 잠시 멈췄다가 Idle 전환
        if (distance > detectRange)
        {
            ai.Movement.StopMonster();
            ai.Animation.SetStop(direction);
        }

        // 인심 범위 내일 때 쫓아가기
        else
        {
            ai.Movement.Move(playerRandomPosition);
            ai.Animation.SetMove(direction);
        }
    }

    public void Update()
    {
        if(locateTimer <= 0f)
        {
            playerRandomPosition = (Vector2)PlayerController.player.transform.position + Random.insideUnitCircle * attackRange;
            locateTimer = locateTime;
        }
        locateTimer -= Time.deltaTime;
    }
}
