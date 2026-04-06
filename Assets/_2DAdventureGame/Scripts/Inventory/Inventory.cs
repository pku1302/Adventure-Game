using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IPointerClickHandler
{
    public List<InventorySlot> slots = new List<InventorySlot>();

    [Header("UI")]
    public Transform slotParent; // Panel
    public GameObject slotPrefab; // Slot Prefab
    public int slotCount = 10;
    public ItemData testItem;
    public ItemData testItem2;
    public Transform quickSlotParent;

    private List<InventorySlotUI> slotUIs = new List<InventorySlotUI>();
    private InventorySlotUI[] quickSlotUIs = new InventorySlotUI[QuickSlotCount];
    private const int QuickSlotCount = 5;

    private void Start()
    {
        for (int i = 0; i < slotCount + QuickSlotCount; i++)
        {
            slots.Add(new InventorySlot(null, 0));
        }
        CreateSlots();
        UpdateUI();
        AddItem(testItem, 4);
        AddItem(testItem2, 2);
    }

    private void OnDisable()
    {
        TooltipUI.Instance.Hide();
        ContextMenuUI.Instance.Hide();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ContextMenuUI.Instance.Hide();
        }
    }

    void CreateSlots()
    {
        for (int i = 0; i < slotCount; i++)
        {
            GameObject go = Instantiate(slotPrefab, slotParent);
            InventorySlotUI ui = go.GetComponent<InventorySlotUI>();
            slotUIs.Add(ui);
        }

        for (int i = 0; i < QuickSlotCount; i++)
        {
            GameObject go = Instantiate(slotPrefab, quickSlotParent);
            InventorySlotUI ui = go.GetComponent<InventorySlotUI>();
            quickSlotUIs[i] = ui;
            slotUIs.Add(ui);
        }
    }

    public void AddItem(ItemData item, int amount)
    {
        foreach (var slot in slots)
        {
            // 같은 아이템은 카운트 증가
            if (slot.item == item)
            {
                slot.count += amount;
                UpdateUI();
                return;
            }
        }

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

    public void UpdateUI()
    {
        for (int i = 0; i < slotUIs.Count; i++)
        {
            slotUIs[i].SetSlot(this, i, slots[i]);
        }
    }

    public void Merge(int from, int to)
    {
        if (slots[from].item == null || slots[to].item == null) return;

        slots[to].count += slots[from].count;

        slots[from].item = null;
        slots[from].count = 0;
    }

    public void Swap(int a, int b)
    {
        var temp = slots[a];
        slots[a] = slots[b];
        slots[b] = temp;
    }

    public void Add(ItemData item, int count, int to)
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

    public ItemData UseItem(InventorySlot slot, int index)
    {
        if (slot.item == null) return null;
        if (slot.item is ConsumableItem consumable)
        {
            slot.count--;
            Debug.Log("아이템 사용됨");

            if (slot.count <= 0)
            {
                slots[index].item = null;
            }
        }
        else
        {
            Debug.Log("사용 불가");
        }
        UpdateUI();

        return slot.item;
    }

    public ItemData GetItem(int index)
    {
        return slots[index].item;
    }

    public void RemoveItem(int index, bool isDrop)
    {
        if (isDrop)
        {
            slots[index].item = null;
            slots[index].count = 0;
        }
        else
        {
            slots[index].count -= 1;

            if (slots[index].count == 0)
                slots[index].item = null;
        }

        UpdateUI();
    }
}
