using UnityEngine;
using UnityEngine.UI;

public class BuffIconUI : MonoBehaviour
{
    public Image icon;
    public void Init(StatusEffect effect)
    {
        icon.sprite = effect.data.icon;
    }
}
