using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class StatusEffectManager : MonoBehaviour
{
    public Action<StatusEffect> OnEffectAdded;
    public Action<StatusEffect> OnEffectRemoved;
    public Action<StatusEffect> OnEffectUpdated;
    public Action<StatusEffect> OnEffectActivated;
    public Action<HealBuff> OnHealEffectAdded;

    public Action<PoisonDebuff> OnPoisonDebuffAdded;
    public Action OnPoisonDebuffRemoved;

    public PlayerHealth health;
    private List<StatusEffect> effects = new List<StatusEffect>();
    private void Awake()
    {
        health = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        float dt = Time.deltaTime;

        for (int i = effects.Count - 1; i >= 0; i--)
        {
            var effect = effects[i];
            effect.OnUpdate(this, dt);

            if (effect.IsFinished)
            {
                RemoveEffect(effect);
                if (effect is PoisonDebuff posion)
                {
                    OnPoisonDebuffRemoved?.Invoke();
                }
            }
        }
    }

    public void AddEffect(StatusEffect effect)
    {
        var existing = effects.Find(e => e.GetType() == effect.GetType());

        if (existing != null)
        {
            existing.OnStackOrRefresh(this);
            OnEffectUpdated?.Invoke(existing);
        }
        else
        {
            effect.OnApply(this);
            effects.Add(effect);
            OnEffectAdded?.Invoke(effect);
            if (effect is HealBuff healEfect)
            {
                OnHealEffectAdded?.Invoke(healEfect);
            }
        }
    }

    public void RemoveEffect(StatusEffect effect)
    {
        effect.OnRemove(this);
        effects.Remove(effect);
        OnEffectRemoved?.Invoke(effect);
    }

    public void NotifyEffectActivated(StatusEffect effect)
    {
        OnEffectActivated?.Invoke(effect);
        if (effect is PoisonDebuff poisonDebuff)
        {
            OnPoisonDebuffAdded?.Invoke(poisonDebuff);
        }
    }
}
