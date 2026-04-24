using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LootSlotUI : ItemSlotUI
{
    private LootUI lootUI;
    private InventoryUI playerInventory;

    public override void SetSlot(ItemStorage container, ItemSlot data, int index)
    {
        slot = data;
        storage = container;
        lootUI = (LootUI)container;
        this.index = index;

        if (data?.item != null)
        {
            icon.sprite =  data.item.icon;
            countText.text = data.count > 1 ? data.count.ToString() : "";
            background.color = RarityColorUtility.GetColor(slot.item.rarity);
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
            playerInventory.AddItem(slot.item, slot.count);

            lootUI.currentLoot.lootItems.Remove(slot);

            lootUI.Refresh();
        }
    }

    public void OnDragEnd()
    {
        lootUI.currentLoot.lootItems[index] = null;
        lootUI.Refresh();
    }

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
    }
}
