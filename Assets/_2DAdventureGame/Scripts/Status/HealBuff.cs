using UnityEngine;

public class HealBuff : StatusEffect
{
    private float tickTimer = 0f;
    public float amount;
    public const float Tick = 2f;

    public HealBuff(StatusEffectData data, int amount)
    {
        this.data = data;
        this.amount = amount;
    }

    public float GetCurrentHealAmount()
    {
        return amount * ((data.duration - elapsed) / Tick);
    }

    public override void OnUpdate(StatusEffectManager target, float deltaTime)
    {
        base.OnUpdate(target, deltaTime);

        tickTimer += deltaTime;

        if (tickTimer >= Tick)
        {
            target.health.TakeHeal(amount);
            tickTimer = 0f;
        }
    }

    protected override void OnMaxStackReached(StatusEffectManager target)
    {
        base.OnMaxStackReached(target);
    }
}
