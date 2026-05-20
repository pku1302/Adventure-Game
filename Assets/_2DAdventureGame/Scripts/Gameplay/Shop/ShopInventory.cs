using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class ShopContainer : ItemContainer
{
    public override void Init(List<ItemData> stockItems)
    {
        foreach (var itemData in stockItems)
        {
            items.Add(new InventoryItem(itemData, 1));
        }
    }
}
