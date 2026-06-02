using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance;
    public GameObject root;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI additionalText;

    private void Start()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void Show(ItemData item, RectTransform iconRect, ItemSlotUI slot)
    {
        root.SetActive(true);

        root.transform.position = iconRect.position;
        descriptionText.text = item.description;
        if (slot is ShopSlotUI && slot.GetContainer() is ShopContainer)
        {
            additionalText.text = "구입가격: " + item.buyPrice.ToString() + "$";
        }
        else
        {
            additionalText.text = "판매가격: " + item.sellPrice.ToString() + "$";
        }
    }

    public void Hide()
    {
        root.SetActive(false);
    }
}
