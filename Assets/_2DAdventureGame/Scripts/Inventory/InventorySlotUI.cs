using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IDragSource, IPointerEnterHandler, IPointerExitHandler
{
    public int index;
    public Image icon;
    public Image background;
    public TMP_Text countText;

    private InventorySlot slot;
    private static InventorySlotUI selectedSlot;
    private bool isHovering = false;

    private static bool isSplitMode;
    private static int splitAmount;
    private Inventory inventory;

    public void SetSlot(Inventory inv, int i, InventorySlot slotData)
    {
        inventory = inv;
        index = i;
        slot = slotData;

        if (slot.item != null)
        {
            icon.sprite = slot.item.icon;
            countText.text = slot.count > 1 ? slot.count.ToString() : "";
            background.color = RarityColorUtility.GetColor(slotData.item.rarity);
        }
        else
        {
            background.color = Color.white;
            icon.sprite = null;
            countText.text = "";
        }
    }

    // 호버시 툴팁
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isHovering) return;

        isHovering = true;

        if (slot.item != null)
            TooltipUI.Instance.Show(slot.item, transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        TooltipUI.Instance.Hide();
    }

    // 드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slot == null || slot.item == null) return;

        DragData.DragSource = this;

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

    // 드롭 
    public void OnDrop(PointerEventData eventData)
    {
        if (DragData.DragSource == null || DragData.DragSource == this)
        {
            return;
        }

        var toSlot = inventory.slots[index];

        if (DragData.DragSource is InventorySlotUI invSource)
        {
            int from = invSource.index;
            int to = index;

            var fromSlot = inventory.slots[from];

            if (isSplitMode)
            {
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
            }
            else
            {
                if (toSlot.item != null &&
                    fromSlot.item == toSlot.item &&
                    fromSlot.item.maxStack > 1)
                {
                    inventory.Merge(from, to);
                }
                else
                {
                    inventory.Swap(from, to);
                }
            }
        }
        else if (DragData.DragSource is LootSlotUI lootSource)
        {
            inventory.Add(lootSource.slotData.item, lootSource.slotData.count, index);
            lootSource.OnDragEnd();
        }

        DragIconUI.Instance.Hide();
        inventory.UpdateUI();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (slot.item != null && eventData.button == PointerEventData.InputButton.Right)
        {
            ContextMenuUI.Instance.Show(slot, transform.position, index);
        }

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ContextMenuUI.Instance.Hide();
        }
    }
}
