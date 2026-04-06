using UnityEngine;

[CreateAssetMenu(menuName = "Item/Consumable")]
public class ConsumableItem : ItemData
{
    public int healAmount = 2;
    public float useTime;
    public StatusEffectData healData;

    public override bool CanUse()
    {
        return true;
    }

    public override void Use(GameObject player)
    {
        var playerBuffManager = player.GetComponent<StatusEffectManager>();
        playerBuffManager.AddEffect(new HealBuff(healData, healAmount));
    }
}
