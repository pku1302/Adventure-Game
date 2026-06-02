using NUnit.Framework;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using System.Collections.Generic;

public class LootUI : ItemContainerUI
{
    [SerializeField] private LootOverlayController lootOverlay; // °¡¸²¸·
    private ILootPresenter lootPresenter;

    protected override void UpdateUI()
    {
        for (int i = 0; i < slotUIs.Count; i++)
        {
            slotUIs[i].Refresh();
        }
    }
    public void Init(IItemSlotPresenter presenter, ItemContainer container, ILootPresenter lootPresenter)
    {
        ClearSlots();
        this.lootPresenter = lootPresenter;
        base.Init(presenter, container);
    }

    public override void TurnOn()
    {
        gameObject.SetActive(true);
    }

    public override void TurnOff()
    {
        gameObject.SetActive(false);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        lootPresenter.StopLoot();
    }

    protected override void CreateSlotUIs(int index)
    {
        for (int i = 0; i < container.GetSlotCount(); i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            LootSlotUI ui = slot.GetComponent<LootSlotUI>();
            ui.Init(i, presenter, lootPresenter, container);
            slotUIs.Add(ui);
        }
    }

    protected void ClearSlots()
    {
        foreach (var slot in slotUIs)
        {
            Destroy(slot.gameObject);
        }

        slotUIs.Clear();
    }

    public void ShowLootingLoadingUI()
    {
        lootOverlay.Show();
    }

    public void HideLootingLoadingUI()
    {
        lootOverlay.Hide();
    }
}
