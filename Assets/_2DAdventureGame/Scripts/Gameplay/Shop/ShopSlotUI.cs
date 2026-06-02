using UnityEngine;
using UnityEngine.EventSystems;

public class ShopSlotUI : ItemSlotUI
{
    private IShopPresenter shopPresenter;
    private CursorManager cursorManager;

    public void Init(int index, IItemSlotPresenter presenter, IShopPresenter shopPresenter, ItemContainer container, CursorManager cursorManager)
    {
        this.index = index;
        this.presenter = presenter;
        this.shopPresenter = shopPresenter;
        this.container = container;
        this.cursorManager = cursorManager;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (slotItem == null)
            return;

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (shopPresenter.OnRightClickShopSlot(container, index))
            {
                cursorManager.OnClickBuy();
            }
            else
            {
                cursorManager.OnClickFail();
            }
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        cursorManager.SetHandCursor();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        cursorManager.SetDefaultCursor();
    }

    
}
