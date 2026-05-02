using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : ItemSlotUI
{
    private InventoryUI inventoryUI;

    public override void SetSlot(ItemContainerUI container, InventoryItem data, int index)
    {
        inventoryUI = (InventoryUI)container;
        slotItem = data;
        this.containerUI = container;
        this.index = index;
        InitializeContainerManager();

        if (slotItem != null)
        {
            icon.sprite = slotItem.data.icon;
            countText.text = slotItem.count > 1 ? slotItem.count.ToString() : "";
            background.color = RarityColorUtility.GetColor(data.data.rarity);
        }
        else
        {
            background.color = Color.white;
            icon.sprite = null;
            countText.text = "";
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (slotItem != null && eventData.button == PointerEventData.InputButton.Right)
        {
            ContextMenuUI.Instance.Hide();
            if (slotItem.data is EquipmentData e)
            {
                inventoryUI.inventory.EquipItem(slotItem, index);
                inventoryUI.UpdateUI();
                return;
            }
            ContextMenuUI.Instance.Show(slotItem, transform.position, index);
        }

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ContextMenuUI.Instance.Hide();
        }
    }

    public override void InitializeContainerManager()
    {
        containerManager = inventoryUI.inventory;
    }
}
