using UnityEngine;
using UnityEngine.UI;


public class ProgressUI : MonoBehaviour
{
    [SerializeField]
    public Image progressBar;

    private void Awake()
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        progressBar.fillAmount = 0f;
    }

    public void SetProgress(float value)
    {
        progressBar.fillAmount = value;
    }
}
