using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUI : ItemContainerUI
{
    public InventoryManager inventory;

    private void Start()
    {
        CreateSlotUIs();
        UpdateUI();
    }
    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
    }

    public void TurnOn()
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
    protected override void CreateSlotUIs()
    {
        for (int i = 0; i < inventory.slotCount; i++)
        {
            GameObject go = Instantiate(slotPrefab, slotParent);
            InventorySlotUI ui = go.GetComponent<InventorySlotUI>();
            slotUIs.Add(ui);
        }
    }

    // ÀüÃ¼ ½½·Ôµé ¾÷µ¥ÀÌÆ®
    public override void UpdateUI()
    {
        for (int i = 0; i < inventory.GetSlotCount(); i++)
        {
            slotUIs[i].SetSlot(this, inventory.GetSlotItem(i), i);
        }
    }
}
