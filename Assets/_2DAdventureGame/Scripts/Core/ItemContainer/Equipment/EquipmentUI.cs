using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : ItemContainerUI
{
    private IEquipmentPresenter equipmentPresenter;
    public void Init(IItemSlotPresenter presenter, ItemContainer container, IEquipmentPresenter equipmentPresenter)
    {
        this.equipmentPresenter = equipmentPresenter;
        base.Init(presenter, container);
    }

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
            EquipmentSlotUI ui = slot.GetComponent<EquipmentSlotUI>();
            ui.Init(i, presenter, equipmentPresenter, container);
            slotUIs.Add(ui);
        }
    }

    protected override void UpdateUI()
    {
        for (int i = 0; i < slotUIs.Count; i++)
        {
            slotUIs[i].Refresh();
        }
    }
}
