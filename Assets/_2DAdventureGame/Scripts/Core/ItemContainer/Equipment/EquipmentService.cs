using UnityEngine;

public class EquipmentService
{
    public bool Equip(
        Inventory inventory,
        EquipmentContainer equipment,
        PlayerStats stat,
        int inventoryIndex
        )
    {
        var item = inventory.GetSlotItem(inventoryIndex);

        if (item == null || item.data is not EquipmentData e)
            return false;

        if (!equipment.IsEquippable(e))
            return false;

        equipment.Equip(item);

        inventory.RemoveItem(item);

        RecalculateStat(stat, equipment);

        return true;
    }

    public bool Unequip(
    Inventory inventory,
    EquipmentContainer equipment,
    PlayerStats stat,
    int equipIndex
    )
    {
        var item = equipment.GetSlotItem(equipIndex);

        if (item == null || item.data is not EquipmentData e)
            return false;

        if (inventory.IsFull())
            return false;

        equipment.Unequip(equipIndex);

        inventory.AddItem(item.data, 1);

        RecalculateStat(stat, equipment);

        return true;
    }

    public void RecalculateStat(
        PlayerStats stat,
        EquipmentContainer equipment
        )
    {
        stat.totalAttack = stat.baseAttack;
        stat.totalDefense = stat.baseDefense;
        stat.totalMoveSpeed = stat.baseMoveSpeed;

        for(int i = 0; i < equipment.GetSlotCount(); i++)
        {
            InventoryItem e = equipment.GetSlotItem(i);
            if (e == null) continue;
            EquipmentData equipmentData = (EquipmentData)e.data;
            stat.totalAttack += equipmentData.attack;
            stat.totalDefense += equipmentData.defense;
            stat.totalMoveSpeed += equipmentData.speed;
            stat.RecalculateStamina(equipmentData.stamina);
        }
    }
}
