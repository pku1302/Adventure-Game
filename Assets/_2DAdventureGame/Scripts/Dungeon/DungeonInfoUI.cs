using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonInfoUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text nameText;

    [SerializeField]
    private TMP_Text description;

    [SerializeField]
    private TMP_Text warning;

    [SerializeField]
    private Image thumbnail;

    public void Show(DungeonData data)
    {
        gameObject.SetActive(true);
        nameText.text = data.name.ToString();
        description.text = data.description.ToString();
        warning.text = data.warning.ToString();
        thumbnail.sprite = data.thumbnail;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
