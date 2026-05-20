using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public interface IDraggable
{
    public int GetSlotIndex();
    public ItemContainer GetContainer();
    public InventoryItem GetInventoryItem();
}
