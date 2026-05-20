using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotUI : ItemSlotUI
{
    private IInventoryPresenter inventoryPresenter;
    public void Init(int index, IItemSlotPresenter presenter, IInventoryPresenter inventoryPresenter, ItemContainer container)
    {
        this.index = index;
        this.presenter = presenter;
        this.container = container;
        this.inventoryPresenter = inventoryPresenter;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (slotItem == null)
            return;

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (slotItem.data.itemType == ItemType.Equipment)
            {
                inventoryPresenter.EquipItem(index);
            }
            else if (slotItem.data.itemType == ItemType.Consumable)
            {
                inventoryPresenter.OpenContextMenu(index, transform.position);
            }
        }
    }

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
        inventoryPresenter.OnDrop(
            dropResult.from,
            dropResult.to,
            dropResult.fromIndex,
            dropResult.toIndex,
            dropResult.amount,
            isSplitMode
        );
    }

}
