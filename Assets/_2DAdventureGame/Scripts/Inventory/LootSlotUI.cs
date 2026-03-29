using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LootSlotUI : MonoBehaviour, IPointerClickHandler, IDragSource, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private LootUI lootUI;
    private Inventory playerInventory;
    public InventorySlot slotData;
    public Image icon;
    public Image background;
    public TMP_Text countText;
    public int index;

    public void SetSlot(InventorySlot data, LootUI ui, Inventory inventory, int idx)
    {
        slotData = data;
        lootUI = ui;
        playerInventory = inventory;
        index = idx;

        if (data?.item != null)
        {
            icon.sprite =  data.item.icon;
            countText.text = data.count > 1 ? data.count.ToString() : "";
        }
        else
        {
            icon.sprite = null;
            countText.text = "";
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            playerInventory.AddItem(slotData.item, slotData.count);

            lootUI.currentLoot.lootItems.Remove(slotData);

            lootUI.Refresh();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slotData == null || slotData.item == null) return;

        DragData.DragSource = this;
        DragIconUI.Instance.Show(icon.sprite);
        DragIconUI.Instance.countText.text = slotData.count > 1 ? slotData.count.ToString() : "";
    }

    public void OnDrag(PointerEventData eventData)
    {
        DragIconUI.Instance.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragIconUI.Instance.Hide();
    }


    public void OnDragEnd()
    {
        lootUI.currentLoot.lootItems[index] = null;
        lootUI.Refresh();
    }

    public void OnDrop(PointerEventData eventData)
    {
    


    }
}
