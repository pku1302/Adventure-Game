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


}
