using Unity.VisualScripting;
using UnityEngine;

public class PoisonDebuff : StatusEffect
{
    private float damage = 3f;
    private float tickTimer = 0f;

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

            if (tickTimer >= 5f)
            {
                target.health.TakeDamage(damage);
                tickTimer = 0f;
            }
        }
    }

    protected override void OnMaxStackReached(StatusEffectManager target)
    {
        base.OnMaxStackReached(target);
    }
}
