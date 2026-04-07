using UnityEngine;

public abstract class StatusEffect
{
    public StatusEffectData data;

    public int stack = 1;
    public string effectID => data.effectID;
    public Sprite icon => data.icon;

    public float elapsed;
    protected bool isActivated = false;
    public bool IsFinished => elapsed >= data.duration + 0.2f;
    public bool IsActivated => stack >= data.maxStack;

    public virtual void OnApply(StatusEffectManager target) 
    {
        if (stack >= data.maxStack && !isActivated)
        {
            OnMaxStackReached(target);
        }
    }
    public virtual void OnRemove(StatusEffectManager target) { }

    public virtual void OnUpdate(StatusEffectManager target, float deltaTime)
    {
        elapsed += deltaTime;
    }

    public virtual void AddStack(StatusEffectManager target)
    {
        stack = stack == data.maxStack ? data.maxStack : stack + 1;

        if (stack >= data.maxStack && !isActivated)
        {
            OnMaxStackReached(target);
        }
    }

    public virtual void OnStackOrRefresh(StatusEffectManager target)
    {
        AddStack(target);
        elapsed = 0f;
    }

    protected virtual void OnMaxStackReached(StatusEffectManager target)
    {
        isActivated = true;
        target.NotifyEffectActivated(this);
    }
}
