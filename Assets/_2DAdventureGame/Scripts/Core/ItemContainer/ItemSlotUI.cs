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
    [SerializeField] protected Image icon;
    [SerializeField] protected Image background;
    [SerializeField] protected TMP_Text countText;

    protected ItemContainer container;
    protected InventoryItem slotItem; // 슬롯에 담긴 아이템
    protected int index;
    protected IItemSlotPresenter presenter;

    protected
    private bool isHovering = false;
    private static int splitAmount;
    protected static bool isSplitMode;

    public int GetSlotIndex()
    {
        return index;
    }

    public ItemContainer GetContainer()
    {
        return container;
    }

    public InventoryItem GetInventoryItem()
    {
        return slotItem;
    }

    // 슬롯에 아이템을 담고 초기화
    public virtual void SetSlot(InventoryItem data, int index)
    {
        slotItem = data;
        this.index = index;

        if (slotItem != null)
        {
            icon.sprite = slotItem.data.icon;
            countText.text = slotItem.count > 1 ? slotItem.count.ToString() : "";
            background.color = RarityColorUtility.GetColor(data.data.rarity);
        }
        else
        {
            background.color = Color.white;
            icon.sprite = null;
            countText.text = "";
        }
    }

    public void Refresh()
    {
        slotItem = presenter.GetSlotData(container, index);
        SetSlot(slotItem, index);
    }

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
        DragIconUI.Instance.Show(icon.sprite);
        DragIconUI.Instance.countText.text = splitAmount > 1 ? splitAmount.ToString() : "";
    }

    // 드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        DragIconUI.Instance.transform.position = eventData.position;
    }

    // 드래그 끝
    public void OnEndDrag(PointerEventData eventData)
    {
        DragIconUI.Instance.Hide();
        isSplitMode = false;
        splitAmount = 0;
    }

    public class DropResult
    {
        public ItemContainer from;
        public ItemContainer to;
        public InventoryItem fromItem;
        public int fromIndex;
        public int toIndex;
        public int amount;
    }

    protected DropResult dropResult = new DropResult();

    // 슬롯 위로 드랍되었을 때]
    public virtual void OnDrop(PointerEventData eventData)
    {
        if (DragData.Draggable == null)
        {
            return;
        }

        dropResult.from = DragData.Draggable.GetContainer();
        dropResult.to = container;
        dropResult.fromItem = DragData.Draggable.GetInventoryItem();
        dropResult.fromIndex = DragData.Draggable.GetSlotIndex();
        dropResult.toIndex = index;
        dropResult.amount = isSplitMode ? splitAmount : dropResult.fromItem.count;

        DragIconUI.Instance.Hide();
        DragData.Draggable = null;
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (slotItem == null)
            return;
    }
}
