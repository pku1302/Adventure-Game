using UnityEngine;
using UnityEngine.EventSystems;

public class ShopSlotUI : ItemSlotUI
{
    private IShopPresenter shopPresenter;

    public void Init(int index, IItemSlotPresenter presenter, IShopPresenter shopPresenter, ItemContainer container)
    {
        this.index = index;
        this.presenter = presenter;
        this.shopPresenter = shopPresenter;
        this.container = container;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (slotItem == null)
            return;

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            shopPresenter.OnRightClickShopSlot(container, index);
        }
    }
}
