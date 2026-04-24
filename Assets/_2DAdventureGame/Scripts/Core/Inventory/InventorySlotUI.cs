using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : ItemSlotUI
{
    private InventoryUI inventory;
    public override void SetSlot(ItemStorage container, ItemSlot data, int index)
    {
        storage = container;
        inventory = (InventoryUI)container;
        this.index = index;
        slot = data;

        if (slot.item != null)
        {
            icon.sprite = slot.item.icon;
            countText.text = slot.count > 1 ? slot.count.ToString() : "";
            background.color = RarityColorUtility.GetColor(data.item.rarity);
        }
        else
        {
            background.color = Color.white;
            icon.sprite = null;
            countText.text = "";
        }
    }

    // 인벤토리 슬롯 위로 드롭 
    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (slot.item != null && eventData.button == PointerEventData.InputButton.Right)
        {
            ContextMenuUI.Instance.Show(slot, transform.position, index);
        }

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ContextMenuUI.Instance.Hide();
        }
    }
}
