using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlotUI : ItemSlotUI
{
    [SerializeField]
    private Sprite emptySlotSprite;

    private IEquipmentPresenter equipmentPresenter;

    public void Init(int index, IItemSlotPresenter presenter, IEquipmentPresenter equipmentPresenter, ItemContainer container)
    {
        this.index = index; 
        this.presenter = presenter;
        this.equipmentPresenter = equipmentPresenter;
        this.container = container;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (slotItem == null)
            return;

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            equipmentPresenter.UnequipItem(index);
        }
    }

    public override void SetSlot(InventoryItem data, int index)
    {
        slotItem = data;
        this.index = index;

        if (slotItem != null)
        {
            icon.sprite = slotItem.data.icon;
            background.color = RarityColorUtility.GetColor(data.data.rarity);
        }
        else
        {
            background.color = Color.white;
            icon.sprite = emptySlotSprite;
        }
    }


}
