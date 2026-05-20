using UnityEngine;
using UnityEngine.EventSystems;

public class EnhanceSlotUI : ItemSlotUI
{
    private IEnhancePresenter enhancePresenter;

    public void Init(int index, IItemSlotPresenter presenter, IEnhancePresenter enhancePresenter, ItemContainer container)
    {
        this.index = index;
        this.presenter = presenter;
        this.container = container;
        this.enhancePresenter = enhancePresenter;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (slotItem == null)
            return;

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            enhancePresenter.SelectEnhanceItem(slotItem);
        }
    }

    public override void SetSlot(InventoryItem item, int index)
    {
        if (item == null) return;
        if (item.data is not EquipmentData e || e.nextEnhanceItem == null)
        {
            return;
        }

        slotItem = item;
        this.index = index;

        icon.sprite = slotItem.data.icon;
        background.color = RarityColorUtility.GetColor(e.rarity);
    }
}
