using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ItemContainerUI : MonoBehaviour
{
    protected IItemSlotPresenter presenter;
    protected ItemContainer container;
    protected List<ItemSlotUI> slotUIs = new List<ItemSlotUI>();

    public Transform slotParent; // Panel
    public GameObject slotPrefab; // Slot Prefab

    protected virtual void OnDisable()
    {
        if (TooltipUI.Instance != null)
        {
            TooltipUI.Instance.Hide();
        }
    }

    // 슬롯 UI 생성하기
    protected abstract void CreateSlotUIs(int index);
    // 모든 슬롯 UI 업데이트
    protected virtual void UpdateUI()
    {
        for (int i = 0; i < slotUIs.Count; i++)
        {
            slotUIs[i].Refresh();
        }
    }
    protected virtual void Init(IItemSlotPresenter presenter, ItemContainer container)
    {
        this.presenter = presenter;
        this.container = container;

        CreateSlotUIs(container.GetSlotCount());
        container.OnChanged += UpdateUI;
        UpdateUI();
    }

    public abstract void TurnOn();
    public abstract void TurnOff();


}
