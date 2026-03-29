using UnityEngine;

[CreateAssetMenu(menuName = "Item/Consumable")]
public class ConsumableItem : ItemData
{
    public int healAmount;

    public void Use()
    {
        Debug.Log("체력 회복: " + healAmount);
    }
}
