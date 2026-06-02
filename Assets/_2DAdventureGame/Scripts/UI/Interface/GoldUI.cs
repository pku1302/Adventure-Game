using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    private GoldManager goldManager;
    public TextMeshProUGUI goldText;

    private void Start()
    {
        goldManager = FindFirstObjectByType<GoldManager>();
        if (goldManager != null)
        {
            Refresh(goldManager.gold);
            goldManager.onGoldChanged += Refresh;
        }
    }

    private void OnDestroy()
    {
        goldManager.onGoldChanged -= Refresh;
    }

    void Refresh(int value)
    {
        goldText.text = value.ToString();
    }

}
