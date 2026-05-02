using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlotUI : ItemSlotUI
{
    private EquipmentUI equipmentUI;
    public EquipmentType equipmentType;

    public override void InitializeContainerManager()
    {
        containerManager = equipmentUI.equipment;
    }

    public override void SetSlot(ItemContainerUI container, InventoryItem data, int index)
    {
        slotItem = data;
        equipmentUI = (EquipmentUI)container;
        if (data != null && data.data is EquipmentData e)
        {
            equipmentType = e.equipmentType;
        }
        containerUI = container;
        InitializeContainerManager();

        if (slotItem != null)
        {
            icon.sprite = slotItem.data.icon;
            countText.text = "";
            background.color = RarityColorUtility.GetColor(slotItem.data.rarity);
        }
        else
        {
            icon.sprite = null;
            countText.text = "";
            background.color = Color.white;
        }
    }

    public override void OnDrop(PointerEventData eventData)
    {
        if (DragData.Draggable == null)
        {
            return;
        }

        int fromIndex = DragData.Draggable.GetSlotIndex();
        ItemData itemData = DragData.Draggable.GetInventoryItem().data;
        ItemContainerUI fromUI = DragData.Draggable.GetSourceUI();
        ContainerManager from = DragData.Draggable.GetContainerManagerRef();
        ContainerManager to = containerManager;

        if (!(itemData is EquipmentData e))
            return;

        if (e.equipmentType != equipmentType)
        {
            Debug.Log("타입 불일치");
            return;
        }

        if (InventorySystem.Swap(from, to, fromIndex, index))
        {
            containerUI.UpdateUI();
            fromUI.UpdateUI();
        }
        DragIconUI.Instance.Hide();
        DragData.Draggable = null;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            EquipmentData e = slotItem.data as EquipmentData;
            equipmentUI.equipment.Unequip(e.equipmentType);
            equipmentUI.inventory.AddItem(e, 1);

            equipmentUI.UpdateUI();
            equipmentUI.inventoryUI.UpdateUI();
        }
    }
}
