using UnityEngine;

public class InventoryForEnhanceUI : ItemContainerUI
{
    private IEnhancePresenter enhancePresenter;

    public void Init(IItemSlotPresenter presenter, ItemContainer container, IEnhancePresenter enhancePresenter)
    {
        ClearSlots();
        this.enhancePresenter = enhancePresenter;
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


    public override void TurnOff()
    {
    }

    public override void TurnOn()
    {
    }

    protected override void CreateSlotUIs(int index)
    {
        for (int i = 0; i < container.GetSlotCount(); i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            EnhanceSlotUI ui = slot.GetComponent<EnhanceSlotUI>();
            ui.Init(i, presenter, enhancePresenter, container);
            slotUIs.Add(ui);
        }
    }

    public void RefreshUI()
    {
        UpdateUI();
    }

    protected override void UpdateUI()
    {
        for (int i = 0; i < slotUIs.Count; i++)
        {
            slotUIs[i].Refresh();
        }
    }
}
