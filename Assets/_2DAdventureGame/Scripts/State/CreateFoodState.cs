using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CreateFoodState : IState
{
    private float timer = 1f;
    private WorkMonsterAIComponent ai;

    public CreateFoodState(WorkMonsterAIComponent ai)
    {
        this.ai = ai;
    }

    public void Enter()
    {
        ai.Animation.SetCreateFood();
        ai.Movement.StopMonster();
    }

    public void Exit()
    {
        ai.Animation.SetCreateFoodDone();
        CreateFood();
    }

    public void FixedUpdate()
    {
    }

    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            ai.ChangeState(new WanderState(ai));
        }
    }

    public void CreateFood()
    {
        Vector2 spawnPos = (Vector2)ai.transform.position + ai.Animation.lastDirection * 1f;
        SpawnManager.instance.Spawn(ai.foodPrefab, spawnPos);
    }
}
