using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayItemSlotUI : MonoBehaviour
{
    [SerializeField] protected Image icon;
    [SerializeField] protected Image background;
    [SerializeField] protected TMP_Text countText;

    public void Set(InventoryItem item)
    {
        if (item == null)
        {
            Clear();
            return;
        }
        icon.sprite = item.data.icon;
        countText.text = item.count > 1 ? item.count.ToString() : "";
        background.color = RarityColorUtility.GetColor(item.data.rarity);
    }

    public void Clear()
    {
        background.color = Color.white;
        icon.sprite = null;
        countText.text = "";
    }
}
