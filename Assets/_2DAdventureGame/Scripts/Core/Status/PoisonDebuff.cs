using Unity.VisualScripting;
using UnityEngine;

public class PoisonDebuff : StatusEffect
{
    private float damage = 3f;
    private float tickTimer = 0f;
    private float Tick = 5f;

    public PoisonDebuff(StatusEffectData data)
    {
        this.data = data;
    }

    public override void OnUpdate(StatusEffectManager target, float deltaTime)
    {
        base.OnUpdate(target, deltaTime);

        if (isActivated)
        {
            tickTimer += deltaTime;

            if (tickTimer >= Tick)
            {
                target.health.TakeDamage(damage, DamageType.Poison);
                tickTimer = 0f;
            }
        }
    }

    public float GetCurrentPoisonAmount()
    {
        return ((data.duration - elapsed) * damage / Tick);
    }

    protected override void OnMaxStackReached(StatusEffectManager target)
    {
        base.OnMaxStackReached(target);
    }
}
