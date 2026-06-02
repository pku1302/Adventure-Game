using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DisplayItemSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Image icon;
    [SerializeField] protected Image background;
    [SerializeField] protected TMP_Text countText;
    [SerializeField] private RectTransform tooltipAnchor;

    private bool isHovering = false;
    private ItemData itemData;

    public void Set(InventoryItem item)
    {
        if (item == null)
        {
            Clear();
            return;
        }
        itemData = item.data;
        icon.sprite = item.data.icon;
        countText.text = item.count > 1 ? item.count.ToString() : "";
        background.color = RarityColorUtility.GetColor(item.data.rarity);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isHovering) return;

        isHovering = true;

        if (TooltipUI.Instance != null && itemData != null)
            TooltipUI.Instance.Show(itemData, tooltipAnchor, null);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;

        if (TooltipUI.Instance != null && itemData != null)
            TooltipUI.Instance.Hide();
    }

    public void Clear()
    {
        itemData = null;
        background.color = Color.white;
        icon.sprite = null;
        countText.text = "";
    }
}
