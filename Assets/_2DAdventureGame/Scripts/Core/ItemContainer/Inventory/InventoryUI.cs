using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUI : ItemContainerUI
{
    private IInventoryPresenter inventoryPresenter;

    public void Init(IItemSlotPresenter presenter, ItemContainer container, IInventoryPresenter invPresenter)
    {
        inventoryPresenter = invPresenter;
        base.Init(presenter, container);
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public override void TurnOff()
    {
        gameObject.SetActive(false);
    }

    public override void TurnOn()
    {
        gameObject.SetActive(true);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (ContextMenuUI.Instance != null )
        {
            ContextMenuUI.Instance.Hide();
        }
    }

    // ÀÎº¥ ºó °÷ Å¬¸¯ ½Ã ÆË¾÷ ¸Þ´º ´Ý±â
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ContextMenuUI.Instance.Hide();
        }
    }

    // ºó ½½·Ô ¸¸µé±â
    protected override void CreateSlotUIs(int index)
    {
        slotUIs.Clear();

        for (int i = 0; i < container.GetSlotCount(); i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            InventorySlotUI ui = slot.GetComponent<InventorySlotUI>();
            ui.Init(i, presenter, inventoryPresenter, container);
            slotUIs.Add(ui);
        }
    }

    // ÀüÃ¼ ½½·Ôµé ¾÷µ¥ÀÌÆ®
    protected override void UpdateUI()
    {
        for (int i = 0; i < slotUIs.Count; i++)
        {
            slotUIs[i].Refresh();
        }
    }
}
