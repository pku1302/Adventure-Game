using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class InventorySlotUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public int index;
    public Image icon;
    public Image background;
    public TMP_Text countText;

    private InventorySlot slot;
    private static InventorySlotUI selectedSlot;
    private static InventorySlotUI draggedSlot;
    private static RectTransform dragRect;
    private static TMP_Text dragText;
    private static Image dragIcon;
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
        }
        else
        {
            icon.sprite = null;
            countText.text = "";
        }
    }

    // 드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slot == null || slot.item == null) return;

        draggedSlot = this;

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

        dragRect.gameObject.SetActive(true);
        dragIcon.sprite = icon.sprite;

        if (dragText != null)
        {
            dragText.text = splitAmount > 1 ? splitAmount.ToString() : "";
        }
    }

    // 드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        dragRect.position = eventData.position;
    }

    // 드래그 끝
    public void OnEndDrag(PointerEventData eventData)
    {
        dragRect.gameObject.SetActive(false);

        if (dragText != null)
            dragText.text = "";

        isSplitMode = false;
        splitAmount = 0;
    }

    // 드롭 
    public void OnDrop(PointerEventData eventData)
    {
        if (draggedSlot == null || draggedSlot == this)
        {
            return;
        }

        int from = draggedSlot.index;
        int to = index;

        var fromSlot = inventory.slots[from];
        var toSlot = inventory.slots[to];

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
                fromSlot.item.isStackable)
            {
                inventory.Merge(from, to);
            }
            else
            {
                inventory.Swap(from, to);
            }
        }
        inventory.UpdateUI();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (slot != null && slot.item != null)
        {
            Select(); 
        }
    }

    void Select()
    {
        if (selectedSlot != null)
        {
            selectedSlot.Deselect();
        }

        selectedSlot = this;
        background.color = Color.yellow;
    }

    void Deselect()
    {
        background.color = Color.white;
    }

    public static void SetDragIcon(RectTransform root, Image img, TMP_Text text)
    {
        dragIcon = img;
        dragText = text;
        dragRect = root;
    }


}
