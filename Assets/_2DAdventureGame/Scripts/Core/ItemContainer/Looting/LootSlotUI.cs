using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LootSlotUI : ItemSlotUI
{
    private LootUI lootUI;

    public override void SetSlot(ItemContainerUI container, InventoryItem data, int index)
    {
        slotItem = data;
        this.containerUI = container;
        lootUI = (LootUI)container;
        this.index = index;
        InitializeContainerManager();

        if (slotItem != null)
        {
            icon.sprite =  slotItem.data.icon;
            countText.text = data.count > 1 ? data.count.ToString() : "";
            background.color = RarityColorUtility.GetColor(slotItem.data.rarity);
        }
        else
        {
            icon.sprite = null;
            countText.text = "";
            background.color = Color.white;
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (slotItem == null) return;

            lootUI.playerInventory.AddItem(slotItem.data, slotItem.count);
            lootUI.currentLoot.RemoveItem(index);

            lootUI.UpdateUI();
            lootUI.inventoryUI.UpdateUI();
        }
    }

    public override void InitializeContainerManager()
    {
        containerManager = lootUI.currentLoot;
    }
}
