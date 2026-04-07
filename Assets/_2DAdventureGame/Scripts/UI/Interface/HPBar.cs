using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPBar : MonoBehaviour
{
    public Image currentHPBar;
    public Image healHPBar;
    public Image poisonLine;
    public TMP_Text CurrentHP;

    private Color backupColor;
    private HealBuff currentBuff;
    private PoisonDebuff currentPoisonDebuff;

    private void Start()
    {
        backupColor = currentHPBar.color;
        poisonLine.gameObject.SetActive(false);
    }

    public void UpdateHP(float current, float max)
    {
        currentHPBar.fillAmount = current / max;
        CurrentHP.text = current.ToString();

        if (currentBuff != null)
            healHPBar.fillAmount = Mathf.Min(current / max + currentBuff.GetCurrentHealAmount() / max, 1);
        else
            healHPBar.fillAmount = 0f;

        if (currentPoisonDebuff != null)
            UpdatePoisonLine(currentPoisonDebuff);
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
        poisonLine.gameObject.SetActive(false);
    }

    public void RemovePoisonLine()
    {
        currentHPBar.color = backupColor;
        currentPoisonDebuff = null;
        poisonLine.gameObject.SetActive(false);
    }

    public void UpdatePoisonLine(PoisonDebuff effect)
    {
        poisonLine.gameObject.SetActive(true);
        currentPoisonDebuff = effect;
        float width = currentHPBar.rectTransform.rect.width;
        float x = (1f- (effect.GetCurrentPoisonAmount() / 100f + (1 - currentHPBar.fillAmount))) * width;
        poisonLine.rectTransform.anchoredPosition = new Vector2(x, 0f);

        if (x <= 0)
        {
            currentHPBar.color = effect.data.color;
        }
    }

}
