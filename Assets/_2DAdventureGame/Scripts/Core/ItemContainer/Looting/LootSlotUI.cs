using UnityEngine;
using UnityEngine.EventSystems;

public class LootSlotUI : ItemSlotUI
{
    private ILootPresenter lootPresenter;

    public void Init(int index, IItemSlotPresenter presenter, ILootPresenter lootPresenter, ItemContainer container)
    {
        this.index = index;
        this.presenter = presenter;
        this.container = container;
        this.lootPresenter = lootPresenter;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (slotItem == null)
            return;

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            lootPresenter.TakeItem(container, index);
        }
    }

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
        lootPresenter.OnDrop(
            dropResult.from,
            dropResult.to,
            dropResult.fromIndex,
            dropResult.toIndex,
            dropResult.amount,
            isSplitMode
            );
    }
}
