using System.Collections.Generic;
using UnityEngine;

public class StatusComponent : MonoBehaviour
{
    private List<IStatusEffect> effects = new List<IStatusEffect>();

    public void Initialize()
    {
        effects = new List<IStatusEffect>();
    }

    public void AddStatus(IStatusEffect newEffect)
    {
        foreach (var effect in effects)
        {
            if (effect.GetType() == newEffect.GetType())
            {
                return;
            }
        }
        effects.Add(newEffect);
        newEffect.Enter();
    }

    public void RemoveStatus(IStatusEffect status)
    {
        status.Exit();
        effects.Remove(status);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        for (int i = effects.Count - 1; i >= 0; i--)
        {
            effects[i].Update();

            if (effects[i].IsFinished)
            {
                RemoveStatus(effects[i]);
            }
        }
    }
}
