using UnityEngine;
using UnityEngine.UI;


public class UseProgressUI : MonoBehaviour
{
    public Image progressBar;
    public PlayerItem itemHandler;

    private void Start()
    {
        itemHandler.OnUseProgress += UpdateUI;
        itemHandler.OnUseStart += Show;
        itemHandler.OnUseEnd += Hide;
    }

    private void Show()
    {
        progressBar.gameObject.SetActive(true);
    }

    private void Hide()
    {
        progressBar.gameObject.SetActive(false);
    }

    void UpdateUI(float value)
    {
        progressBar.fillAmount = value;
    }
}
