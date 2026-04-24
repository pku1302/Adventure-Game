using System.Threading;
using UnityEngine;

[System.Serializable]
public class ItemSlot
{
    public ItemData item;
    public int count;

    public ItemSlot(ItemData item, int count)
    {
        this.item = item;
        this.count = count;
    }
}
