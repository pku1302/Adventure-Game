using TMPro;
using UnityEngine;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance;
    public Vector2 offset = new Vector2(3000f, -10f);

    public GameObject root;
    public TextMeshProUGUI descriptionText;

    
    void Awake()
    {
        Instance = this;
        Hide();
    }

    public void Show(ItemData item, Vector3 pos)
    {
        root.SetActive(true);

        root.transform.position = pos + new Vector3(164f, -40f);
        descriptionText.text = item.description;
    }

    public void Hide()
    {
        root.SetActive(false);
    }
}
