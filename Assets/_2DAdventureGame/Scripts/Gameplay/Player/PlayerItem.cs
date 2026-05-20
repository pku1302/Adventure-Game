using System.Collections;
using UnityEngine;

public class PlayerItem 
{
    private ProgressController progressController;
    private StatusEffectManager effectManager;
    public bool isUsing { get; private set; }

    private ConsumableData consumable;

    public System.Action<float> OnUseProgress;
    public System.Action OnUseStart;
    public System.Action OnUseEnd;

    public PlayerItem(ProgressController progressController)
    {
        this.progressController = progressController;
    }

    public void Init(StatusEffectManager effectManager)
    {
        this.effectManager = effectManager;
    }

    public void UseItem(ItemData item)
    {
        if (isUsing) return;

        if (item == null) return;

        if (item is ConsumableData consumableItem)
        {
            consumable = consumableItem;
            isUsing = true;

            progressController.Begin(
                consumableItem.useTime,
                () =>
                {
                    EndUse();
                });
        }
    }

    public void CancelUse()
    {
        if (!isUsing) return;

        isUsing = false;
        OnUseProgress?.Invoke(0f);
        progressController.Cancel();
    }

    private void EndUse()
    {
        isUsing = false;
        consumable.Use(effectManager);
        OnUseProgress?.Invoke(0f);
        OnUseEnd?.Invoke();
        progressController.Cancel();
    }
}
