using NUnit.Framework;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class ItemSlotUI : MonoBehaviour
    , IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    , IDropHandler, IDraggable, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public Image background;
    public TMP_Text countText;
    public int index;

    protected ContainerManager containerManager; // 실제 데이터 참조
    protected ItemContainerUI containerUI; // ui 컨테이너 참조
    protected InventoryItem slotItem; // 슬롯에 담긴 아이템
    protected bool isHovering = false;
    protected static bool isSplitMode;
    protected static int splitAmount;

    public int GetSlotIndex()
    {
        return index;
    }

    public ItemContainerUI GetSourceUI()
    {
        return containerUI;
    }

    public ContainerManager GetContainerManagerRef()
    {
        return containerManager;
    }

    public InventoryItem GetInventoryItem()
    {
        return slotItem;
    }

    public abstract void InitializeContainerManager();

    // 슬롯에 아이템을 담고 초기화
    public abstract void SetSlot(ItemContainerUI container, InventoryItem data, int index);

    // 호버 시 툴팁
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isHovering) return;

        isHovering = true;

        if (TooltipUI.Instance != null && slotItem != null)
            TooltipUI.Instance.Show(slotItem.data, transform.position);
    }

    // 호버 아웃 시 툴팁 OFF
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;

        if (TooltipUI.Instance != null && slotItem != null)
            TooltipUI.Instance.Hide();
    }

    // 드래그 시 아이콘이 따라 움직이게
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slotItem == null) return;

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            isSplitMode = true;
        }

        if (isSplitMode && slotItem.count > 1)
        {
            splitAmount = slotItem.count / 2;
        }
        else
        {
            isSplitMode = false;
            splitAmount = slotItem.count;
        }

        DragData.Draggable = this;
        DragIconUI.Instance.countText.text = splitAmount > 1 ? splitAmount.ToString() : "";
        DragIconUI.Instance.Show(icon.sprite);
    }

    // 드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        DragIconUI.Instance.transform.position = eventData.position;
    }

    // 드래그 끝
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        DragIconUI.Instance.Hide();
        isSplitMode = false;
        splitAmount = 0;
    }

    // 슬롯 위로 드랍되었을 때]
    public virtual void OnDrop(PointerEventData eventData)
    {
        if (DragData.Draggable == null)
        {
            return;
        }
        int fromItemAmount = DragData.Draggable.GetInventoryItem().count; 
        int fromIndex = DragData.Draggable.GetSlotIndex();
        ContainerManager from = DragData.Draggable.GetContainerManagerRef();
        ContainerManager to = containerManager;
        ItemContainerUI fromUI = DragData.Draggable.GetSourceUI();
        int amount = isSplitMode ? splitAmount : fromItemAmount;

        if (InventorySystem.Move(from, to, fromIndex, index, amount))
        {
            containerUI.UpdateUI();
            fromUI.UpdateUI();
        }
        DragIconUI.Instance.Hide();
        DragData.Draggable = null;
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
    }
}
