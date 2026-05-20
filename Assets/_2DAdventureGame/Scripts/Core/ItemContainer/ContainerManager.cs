using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class ItemContainer
{
    protected List<InventoryItem> items = new List<InventoryItem>();
    public event Action OnChanged;

    public virtual void Init(int itemCount)
    {
        for (int i = 0; i < itemCount; i++)
        {
            items.Add(null);
        }
    }

    public virtual void Init(List<ItemData> itemDatas)
    {
        foreach(var item in itemDatas)
        {
            items.Add(new InventoryItem(item, 1));
        }
    }

    public virtual void Init(List<InventoryItem> invItems)
    {
        items = invItems;
    }

    public void EventInvoke()
    {
        OnChanged?.Invoke();
    }

    public virtual int GetSlotCount()
    {
        return items.Count;
    }

    public virtual InventoryItem GetSlotItem(int index)
    {
        return items[index];
    }

    public virtual void RemoveItem(InventoryItem item)
    {
        if (item == null)
            return;
        int index = items.FindIndex(i => i == item);
        
        if (index < 0)
            return;
        
        items[index] = null;
        OnChanged?.Invoke();
    }

    public virtual void DecreaseItem(InventoryItem item, int amount)
    {
        if (item == null)
        {
            return;
        }
        item.count -= amount;

        if (item.count <= 0)
        {
            RemoveItem(item);
        }
        OnChanged?.Invoke();
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
                OnChanged?.Invoke();
                return 0;
            }
            // 같은 아이템이라면
            else if (items[i].data == data)
            {
                int spaceLeft = maxStack - items[i].count;
                int addToStack = Mathf.Min(spaceLeft, count);

                items[i].count += addToStack;
                OnChanged?.Invoke();
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
                OnChanged?.Invoke();
                return 0;
            }

            if (items[idx].data == data)
            {
                int spaceLeft = maxStack - items[idx].count;
                int addToStack = Mathf.Min(spaceLeft, count);
                items[idx].count += addToStack;
                count -= addToStack;
                OnChanged?.Invoke();

                if (count == 0)
                    return 0;
            }
        }
        return count;
    }
    public virtual bool CanAdd(ItemData item, int amount, int index)
    {
        if (item == null) return false;
        if (amount <= 0) return false;

        if (index != -1)
        {
            if (index < 0 || index >= items.Count) return false;
            if (items[index] == null) return true;
            if (items[index].data == item) return true;
        }
        else
        {
            foreach (var slot in items)
            {
                if (slot == null) return true;
            }
        }

        return false;
    }

    public virtual bool CanSwap(ItemData item, int index, bool isSplitMode)
    {
        if (item == null) return false;
        if (isSplitMode) return false;
        if (index < 0 || index >= items.Count) return false;
        if (items[index] == null) return false;
        if (items[index].data == item) return false;

        return true;

    }

    private bool IsValidIndex(int index)
    {
        if (index < 0 || index >= items.Count) return false;

        return true;
    }

    public virtual bool Swap(int indexA, int indexB)
    {
        if (indexA == indexB)
            return false;

        if (!IsValidIndex(indexA) || !IsValidIndex(indexB))
        {
            return false;
        }

        var a = items[indexA];
        var b = items[indexB];

        items[indexA] = b;
        items[indexB] = a;

        OnChanged?.Invoke();
        return true;
    }

    public virtual void Clear()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i] = null;
        }
        EventInvoke();
    }
}
