using UnityEngine;

public class InventoryItem
{
    public ItemData data;
    public int count;
    
    public InventoryItem(ItemData data, int count = 1)
    {
        this.data = data;
        this.count = count;
    }
}
