using UnityEngine;

public class StarvingEffect : IStatusEffect
{
    private AttackMonsterAIComponent ai;
    private float duration = 2f;
    public bool IsFinished => duration <= 0;

    public StarvingEffect(AttackMonsterAIComponent ai)
    {
        this.ai = ai;
    }

    public void Enter()
    {
       
    }

    public void Exit()
    {
        ai.Health.TakeDamage(Mathf.RoundToInt(ai.Health.MaxHp * 0.1f));
    }

    public void Update()
    {
        duration -= Time.deltaTime; 
    }
}
