using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : ItemStorage
{
    [Header("UI")]
    public ItemData testItem;
    public ItemData testItem2;
    public Transform quickSlotParent;

    private QuickSlotUI[] quickSlotUIs = new QuickSlotUI[QuickSlotCount];
    private const int QuickSlotCount = 5;

    private void Start()
    {
        slotCount = 10;
        for (int i = 0; i < slotCount + QuickSlotCount; i++)
        {
            slots.Add(new ItemSlot(null, 0));
        }
        CreateSlotUIs();
        UpdateUI();
        AddItem(testItem, 4);
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
    }

    public void TurnOn()
    {
        gameObject.SetActive(true);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        ContextMenuUI.Instance.Hide();
    }

    // 인벤 빈 곳 클릭 시 팝업 메뉴 닫기
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ContextMenuUI.Instance.Hide();
        }
    }

    protected override void CreateSlotUIs()
    {
        for (int i = 0; i < slotCount; i++)
        {
            GameObject go = Instantiate(slotPrefab, slotParent);
            InventorySlotUI ui = go.GetComponent<InventorySlotUI>();
            slotUIs.Add(ui);
        }
    }

    public override void AddItem(ItemData item, int amount)
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

    public ItemData UseItem(ItemSlot slot, int index)
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
