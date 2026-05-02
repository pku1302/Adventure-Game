using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : ContainerManager
{
    public EquipmentManager equipmentManager;
    public PlayerItem player;

    public void EquipItem(InventoryItem equip, int index)
    {
        if (equip.data is not EquipmentData e)
        {
            return;
        }

        RemoveItem(index);
        InventoryItem equipped = equipmentManager.GetEquipment(e.equipmentType);
        if (equipped != null)
        {
            equipmentManager.Unequip(e.equipmentType);
            AddItem(equipped.data, 1, index);
        }
        equipmentManager.Equip(equip);
    }

    public void UseItem(InventoryItem item, int index)
    {
        if (item == null) return;

        player.UseItem(item.data);

        if (item.count <= 0)
        {
            RemoveItem(index);
        }
    }
}
