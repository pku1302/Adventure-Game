using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ItemContainerUI : MonoBehaviour
{
    public Transform slotParent; // Panel
    public GameObject slotPrefab; // Slot Prefab

    protected List<ItemSlotUI> slotUIs = new List<ItemSlotUI>();

    protected virtual void OnDisable()
    {
        if (TooltipUI.Instance != null)
        {
            TooltipUI.Instance.Hide();
        }
    }

    // 슬롯 UI 생성하기
    protected abstract void CreateSlotUIs();
    // 모든 슬롯 UI 업데이트
    public abstract void UpdateUI();
}
