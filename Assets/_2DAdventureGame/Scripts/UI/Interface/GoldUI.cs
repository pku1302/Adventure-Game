using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    public GoldManager goldManager;
    public TextMeshProUGUI goldText;

    private void Start()
    {
        Refresh(goldManager.gold);
        goldManager.onGoldChanged += Refresh;
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
