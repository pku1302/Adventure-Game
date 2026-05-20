using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class EquipmentContainer : ItemContainer
{
    public void Equip(InventoryItem item)
    {
        if (item.data is not EquipmentData e)
            return;

        int index = GetEmptyIndex();
        if (index >= 0)
        {
            items[index] = item;
        }
        EventInvoke();
    }

    public void Unequip(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            items[index] = null;
        }
        EventInvoke();
    }
    public bool IsFull()
    {
        int count = 0;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
                count++;
        }
        return count == items.Count;
    }

    public bool IsEquippable(EquipmentData e)
    {
        if (IsFull()) return false;

        for(int i = 0; i < items.Count; ++i)
        {
            if (items[i] != null && items[i].data is EquipmentData equipped)
            {
                if (equipped.id == e.id)
                    return false;
            }
        }

        return true;
    }

    public int GetEmptyIndex()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
                return i;
        }

        return -1;
    }


    public override void Clear()
    {
        for (int i = 0; i <= items.Count; ++i)
        {
            Unequip(i);
        }
    }
}
