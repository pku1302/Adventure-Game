using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ItemStorage : MonoBehaviour
{
    public List<ItemSlot> slots = new List<ItemSlot>();

    public Transform slotParent; // Panel
    public GameObject slotPrefab; // Slot Prefab
    public int slotCount;

    protected List<ItemSlotUI> slotUIs = new List<ItemSlotUI>();

    // 빈 슬롯으로 모두 채우기
    protected virtual void InitializeSlots()
    {
        for (int i = 0; i < slotCount; i++)
        {
            slots.Add(new ItemSlot(null, 0));
        }
    }

    protected virtual void OnDisable()
    {
        TooltipUI.Instance.Hide();
    }

    // 슬롯 UI 생성하기
    protected abstract void CreateSlotUIs();

    public virtual void AddItem(ItemData item, int amount)
    {
        // 같은 아이템은 카운트 증가
        foreach (var slot in slots)
        {
            if (slot.item == item)
            {
                slot.count += amount;
                UpdateUI();
                return;
            }
        }

        // 비어있으면 그냥 대입
        foreach (var slot in slots)
        {
            if (slot.item == null)
            {
                slot.item = item;
                slot.count = amount;
                UpdateUI();
                return;
            }
        }
    }

    // 모든 슬롯 UI 업데이트
    public virtual void UpdateUI()
    {
        for (int i = 0; i < slotUIs.Count; i++)
        {
            slotUIs[i].SetSlot(this, slots[i], i);
        }
    }

    // 슬롯 데이터 합치기
    public virtual void Merge(int from, int to)
    {
        if (slots[from].item == null || slots[to].item == null) return;

        slots[to].count += slots[from].count;

        slots[from].item = null;
        slots[from].count = 0;
    }

    // 슬롯 데이터 교환
    public virtual void Swap(int a, int b)
    {
        var temp = slots[a];
        slots[a] = slots[b];
        slots[b] = temp;
    }

    // 빈 슬롯에 카운트만큼 더하기
    public virtual void Add(ItemData item, int count, int to)
    {
        if (slots[to].item == null)
        {
            slots[to].item = item;
            slots[to].count += count;
        }
        else if (slots[to].item == item)
        {
            slots[to].count += count;
        }
    }

}
