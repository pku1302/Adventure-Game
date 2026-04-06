using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPBar : MonoBehaviour
{
    public Image currentHPBar;
    public Image healHPBar;
    public TMP_Text CurrentHP;
    private HealBuff currentBuff;

    public void UpdateHP(float current, float max)
    {
        currentHPBar.fillAmount = current / max;
        CurrentHP.text = current.ToString();

        if (currentBuff != null)
            healHPBar.fillAmount = Mathf.Min(current / max + currentBuff.GetCurrentHealAmount() / max, 1);
        else
            healHPBar.fillAmount = 0f;
    }

    public void UpdateGenerationHP(HealBuff effect)
    {
        currentBuff = effect;
        healHPBar.fillAmount = Mathf.Min(currentHPBar.fillAmount + currentBuff.GetCurrentHealAmount() / 100f, 1);
    }

    public void RemoveGenerationHP(HealBuff effect)
    {
        currentBuff = null;
        healHPBar.fillAmount = 0f;
    }
}
