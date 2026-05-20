using UnityEngine;

public class ShopContainerUI : ItemContainerUI
{
    private IShopPresenter shopPresenter;

    public override void TurnOff()
    {
        gameObject.SetActive(false);
    }

    public override void TurnOn()
    {
        gameObject.SetActive(true);
    }

    protected override void CreateSlotUIs(int index)
    {
        for (int i = 0; i < container.GetSlotCount(); i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            ShopSlotUI ui = slot.GetComponent<ShopSlotUI>();
            ui.Init(i, presenter, shopPresenter, container);
            slotUIs.Add(ui);
        }
    }

    public void Init(IItemSlotPresenter presenter, ItemContainer container, IShopPresenter shopPresenter)
    {
        ClearSlots();
        this.shopPresenter = shopPresenter;
        base.Init(presenter, container);
    }

    protected void ClearSlots()
    {
        foreach (var slot in slotUIs)
        {
            Destroy(slot.gameObject);
        }

        slotUIs.Clear();
    }

    protected override void UpdateUI()
    {
        for (int i = 0; i < slotUIs.Count; i++)
        {
            slotUIs[i].Refresh();
        }
    }
}
