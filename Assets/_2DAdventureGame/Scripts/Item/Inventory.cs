using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>();

    [Header("UI")]
    public Transform slotParent; // Panel
    public GameObject slotPrefab; // Slot Prefab
    public int slotCount = 10;
    public ItemData testItem;
    public ItemData testItem2;
    public RectTransform dragRoot;
    public Image dragIconImage;
    public TMP_Text dragText;

    private List<InventorySlotUI> slotUIs = new List<InventorySlotUI>();

    private void Start()
    {
        InventorySlotUI.SetDragIcon(dragRoot, dragIconImage, dragText);
        for (int i = 0; i< slotCount; i++)
        {
            slots.Add(new InventorySlot(null, 0));
        }
        CreateSlots();
        UpdateUI();
        AddItem(testItem, 4);
        AddItem(testItem2, 2);
    }

    void CreateSlots()
    {
        for (int i = 0; i < slotCount; i++)
        {
            GameObject go = Instantiate(slotPrefab, slotParent);
            InventorySlotUI ui = go.GetComponent<InventorySlotUI>();
            slotUIs.Add(ui);
        }
    }

    public void AddItem(ItemData item, int amount)
    {
        foreach(var slot in slots)
        {
            if (slot.item == item && item.isStackable)
            {
                slot.count += amount;
                UpdateUI();
                return;
            }
        }

        foreach(var slot in slots)
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
}
