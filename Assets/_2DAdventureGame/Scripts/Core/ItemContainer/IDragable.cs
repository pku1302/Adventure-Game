using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public interface IDraggable
{
    public int GetSlotIndex();
    public ItemContainerUI GetSourceUI();
    public ContainerManager GetContainerManagerRef();
    public InventoryItem GetInventoryItem();
    
}
