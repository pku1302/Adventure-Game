using UnityEngine;

[CreateAssetMenu(menuName = "Item/Consumable")]
public class ConsumableData : ItemData, IUsable
{
    public int healAmount;
    public float useTime;
    public StatusEffectData healData;

    public void Use(StatusEffectManager effectManager)
    {
        effectManager.AddEffect(new HealBuff(healData, healAmount));
    }
}
