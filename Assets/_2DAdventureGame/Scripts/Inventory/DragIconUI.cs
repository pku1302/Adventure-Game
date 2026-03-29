using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DragIconUI : MonoBehaviour
{
    public static DragIconUI Instance;

    public Image icon;
    public TMP_Text countText;

    private void Awake()
    {
        gameObject.SetActive(true);
        Instance = this;
    }

    private void Start()
    {
        gameObject.SetActive (false);
    }

    public void Show(Sprite sprite)
    {
        icon.sprite = sprite;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
