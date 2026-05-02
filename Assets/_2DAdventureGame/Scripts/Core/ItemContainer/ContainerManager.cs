using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public abstract class ContainerManager : MonoBehaviour
{
    protected List<InventoryItem> items = new List<InventoryItem>();
    public int slotCount;

    protected void Start()
    {
        for (int i = 0; i < slotCount; i++)
        {
            items.Add(null);
        }
        Init();
    }

    protected virtual void Init()
    {

    }

    public virtual int GetSlotCount()
    {
        return items.Count;
    }

    public virtual InventoryItem GetSlotItem(int index)
    {
        return items[index];
    }

    public virtual void RemoveItem(int index)
    {
        items[index] = null;
    }

    public virtual void DecreaseItem(int index, int count)
    {
        items[index].count -= count;

        if (items[index].count <= 0)
        {
            RemoveItem(index);
        }
    }

    public virtual int AddItem(ItemData data, int count, int? index = null)
    {
        if (data == null) return count;

        int maxStack = data.maxStack;

        // index가 주어진 경우
        if (index is int i && i >= 0 && i < items.Count)
        {
            // 비어있다면
            if (items[i] == null)
            {
                items[i] = new InventoryItem(data, count);
                return 0;
            }
            // 같은 아이템이라면
            else if (items[i].data == data)
            {
                int spaceLeft = maxStack - items[i].count;
                int addToStack = Mathf.Min(spaceLeft, count);

                items[i].count += addToStack;
                return count - addToStack;
            }
            // 이외는 아무것도 안함
            return count;
        }

        // index가 안주어진 경우
        for (int idx = 0; idx < items.Count; idx++)
        {
            if (items[idx] == null)
            {
                items[idx] = new InventoryItem(data, count);
                return 0;
            }

            if (items[idx].data == data)
            {
                int spaceLeft = maxStack - items[idx].count;
                int addToStack = Mathf.Min(spaceLeft, count);
                items[idx].count += addToStack;
                count -= addToStack;

                if (count == 0)
                    return 0;
            }
        }
        return count;
    }
    public virtual bool CanAdd(InventoryItem item, int amount, int index)
    {
        if (item == null) return false;
        if (amount <= 0) return false;
        if (index < 0 || index >= items.Count) return false;

        if (items[index] == null) return true;
        if (items[index].data == item.data) return true;

        return false;
    }
}
