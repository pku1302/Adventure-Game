using UnityEngine;

public class EnhanceService
{
    private Inventory inventory;
    private GoldManager goldManager;

    public EnhanceService(Inventory inventory, GoldManager goldManager)
    {
        this.inventory = inventory; 
        this.goldManager = goldManager;
    }

    public bool Enhance(InventoryItem item)
    {
        if (item == null)
        {
            return false;
        }

        if (item.data is not EquipmentData e)
        {
            return false;
        }

        var nextItem = e.nextEnhanceItem;

        if (nextItem == null)
        {
            return false;
        }

        var cost = e.enhanceCost;

        if (goldManager.gold < cost.gold)
        {
            return false;
        }

        if (!inventory.HasItem(cost.material, cost.materialCount))
        {
            return false;
        }

        goldManager.SpendGold(cost.gold);

        inventory.RemoveItem(cost.material, cost.materialCount);

        item.data = nextItem;

        inventory.EventInvoke();

        return true;
    }
}
