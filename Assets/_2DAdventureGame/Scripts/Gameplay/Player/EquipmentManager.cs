using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : ContainerManager
{
    private Dictionary<EquipmentType, InventoryItem> equipped
        = new Dictionary<EquipmentType, InventoryItem>();

    public InventoryManager inventoryManager;
    public System.Action OnEquipmentChanged;

    public void Equip(InventoryItem newItem)
    {
        if (newItem.data is not EquipmentData e)
            return;

        if (equipped.TryGetValue(e.equipmentType, out var oldItem))
        {
            return;
        }

        var stat = GetComponent<PlayerStats>();
        equipped[e.equipmentType] = newItem;
        stat.attack += e.attack;
        stat.defense += e.defense;

        OnEquipmentChanged?.Invoke();
    }

    public void Unequip(EquipmentType equipmentType)
    {
        if (equipped.TryGetValue(equipmentType, out var item))
        {
            EquipmentData e = item.data as EquipmentData;
            var stat = GetComponent<PlayerStats>();
            stat.attack -= e.attack;
            stat.defense -= e.defense;
            equipped.Remove(equipmentType);
            OnEquipmentChanged?.Invoke();
        }
    }

    public InventoryItem GetEquipment(EquipmentType type)
    {
        equipped.TryGetValue(type, out var item);
        return item;
    }

    public override int AddItem(ItemData data, int count, int? index = null)
    {
        if (data is EquipmentData equip)
        {
            Equip(new InventoryItem(equip, 1));
            return 0;
        }

        return 1;
    }

    public override bool CanAdd(InventoryItem item, int amount, int index)
    {
        if (item == null) return false;
        if (amount <= 0) return false;
        if (index < 0 || index >= GetSlotCount()) return false;
        if (!(item.data is EquipmentData e)) return false;

        return true;
    }

    public override void RemoveItem(int index)
    {
        Unequip((EquipmentType)index);
    }

    public override void DecreaseItem(int index, int count)
    {
        Unequip((EquipmentType)index);
    }

    public override int GetSlotCount()
    {
        return 5;
    }

    public override InventoryItem GetSlotItem(int index)
    {
        return GetEquipment((EquipmentType)index);
    }
}
