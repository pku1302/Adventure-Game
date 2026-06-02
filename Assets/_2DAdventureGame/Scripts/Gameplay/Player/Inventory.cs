using UnityEngine;
using System.Collections.Generic;

public class Inventory : ItemContainer
{
    private PlayerItem player;
    private int usingIndex;
    private InventoryItem usingItem;

    public Inventory(PlayerItem player)
    {
        this.player = player;
        player.OnUseEnd += UseItemComplete;
    }

    public bool IsFull()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
                return false;
        }

        return true;
    }

    public void EquipItem(InventoryItem equip, int index)
    {
        if (equip.data is not EquipmentData e)
        {
            return;
        }
        RemoveItem(equip);
    }

    public void UseItem(InventoryItem item, int index)
    {
        if (item == null) return;

        player.UseItem(item.data);
        usingIndex = index;
        usingItem = item;
        // 이벤트 인보크 필요없어
    }

    private void UseItemComplete()
    {
        DecreaseItem(usingItem, 1);
    }

    public bool HasItem(ItemData itemData, int count)
    {
        int currentCount = 0;

        foreach (var item in items)
        {
            if (item == null)
                continue;

            if (item.data != itemData)
                continue;

            currentCount += item.count;

            if (currentCount >= count)
            {
                return true;
            }
        }

        return false;
    }

    public void RemoveItem(ItemData itemData, int count)
    {
        for (int i = 0; i <items.Count; i++)
        {
            var item = items[i];

            if (item == null)
                continue;

            if (item.data != itemData)
                continue;

            int removeAmount = Mathf.Min(item.count, count);

            item.count -= removeAmount;

            count -= removeAmount;

            if (item.count <= 0)
            {
                items[i] = null;
            }

            if (count <= 0)
            {
                break;
            }
        }

        EventInvoke();
    }
}
