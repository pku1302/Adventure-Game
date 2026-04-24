using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class ItemSlotUI : MonoBehaviour
    , IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    , IDropHandler, IDragSource, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public Image background;
    public TMP_Text countText;
    public int index;

    protected ItemStorage storage;
    protected bool isHovering = false;
    protected ItemSlot slot;
    protected static bool isSplitMode;
    protected static int splitAmount;

    public int GetSlotIndex()
    {
        return index;
    }

    public ItemStorage GetStorageRef()
    {
        return storage;
    }

    // 슬롯에 아이템을 담고 초기화
    public abstract void SetSlot(ItemStorage container, ItemSlot data, int index);

    // 호버 시 툴팁
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isHovering) return;

        isHovering = true;

        if (slot.item != null)
            TooltipUI.Instance.Show(slot.item, transform.position);
    }

    // 호버 아웃 시 툴팁 OFF
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        TooltipUI.Instance.Hide();
    }

    // 드래그 시 아이콘이 따라 움직이게
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slot == null || slot.item == null) return;

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            isSplitMode = true;
        }

        if (isSplitMode && slot.count > 1)
        {
            splitAmount = slot.count / 2;
        }
        else
        {
            isSplitMode = false;
            splitAmount = slot.count;
        }

        DragIconUI.Instance.countText.text = splitAmount > 1 ? splitAmount.ToString() : "";
        DragData.DragSource = this;
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

    // 서로 다른, 같은 스토리지 사이 상호작용
    public void TryTransferItem()
    {
        var toSlot = storage.slots[index];

        int from = DragData.DragSource.GetSlotIndex();
        int to = index;

        var fromSlot = DragData.DragSource.GetStorageRef().slots[from];

        // 분할 모드 (우클릭)
        if (isSplitMode)
        {
            // 드랍 슬롯이 비어있을 때
            if (toSlot.item == null)
            {
                toSlot.item = fromSlot.item;
                toSlot.count = splitAmount;

                fromSlot.count -= splitAmount;

                if (fromSlot.count <= 0)
                {
                    fromSlot.item = null;
                    fromSlot.count = 0;
                }
            }
            // 두 슬롯의 아이템 종류가 같을 때
            else if (fromSlot.item == toSlot.item)
            {
                fromSlot.count -= splitAmount;
                toSlot.count += splitAmount;
            }
        }
        else
        {
            if (toSlot.item != null &&
                fromSlot.item == toSlot.item &&
                fromSlot.item.maxStack > 1)
            {
                toSlot.count += fromSlot.count;
                fromSlot.item = null;
                fromSlot.count = 0;
            }
            else if (toSlot.item == null)
            {
                toSlot.item = fromSlot.item;
                toSlot.count = fromSlot.count;
                fromSlot.item = null;
                fromSlot.count = 0;
            }
        }
    }

    // 슬롯 위로 드랍되었을 때
    public virtual void OnDrop(PointerEventData eventData)
    {
        if (DragData.DragSource == null)
        {
            return;
        }
        TryTransferItem();
        storage.UpdateUI();
        DragData.DragSource.GetStorageRef().UpdateUI();
        DragIconUI.Instance.Hide();
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
    }
}
