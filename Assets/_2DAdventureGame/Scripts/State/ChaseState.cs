using UnityEngine;

public class ChaseState : IState
{
    private AttackMonsterAIComponent ai;
    private float attackRange = 1.5f;
    private float detectRange = 5f;
    private float stateConvertDelay = 3f;
    private float delayTimer = 0f;
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
        direction = (PlayerController.player.transform.position - ai.transform.position).normalized;
    }

    public void Exit()
    {
    }

    public void FixedUpdate()
    {
        float distance = Vector2.Distance(ai.transform.position, PlayerController.player.transform.position);

        if (distance <= attackRange)
        {
            ai.ChangeState(new AttackState(ai));
            ai.Animation.SetStop(direction);
            return;
        }

        if (distance > detectRange)
        {
            ai.Movement.StopMonster();
            ai.Animation.SetStop(direction);
            delayTimer += Time.fixedDeltaTime;
            if (delayTimer >= stateConvertDelay)
            {
                ai.ChangeState(new WanderState(ai));
                return;
            }
        }
        else
        {
            ai.Movement.Move(playerRandomPosition);
            delayTimer = 0f;
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
