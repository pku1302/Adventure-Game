using TMPro;
using UnityEngine;

public class TipUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI tipText;
    [SerializeField]
    private Tips[] tips;


    private void Start()
    {
        int index = Random.Range(0, tips.Length);
        tipText.text = tips[index].tip;
    }

}
