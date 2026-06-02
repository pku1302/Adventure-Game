using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Image currentHPBar;
    public Image healHPBar;
    public Image poisonLine;
    public TMP_Text CurrentHP;

    private Color backupColor;
    private HealBuff currentBuff;
    private PoisonDebuff currentPoisonDebuff;
    private float maxHP;

    private void Start()
    {
    }

    public void Init(PlayerStats playerStats)
    {
        maxHP = playerStats.maxHP;
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
        healHPBar.fillAmount = Mathf.Min(currentHPBar.fillAmount + currentBuff.GetCurrentHealAmount() / maxHP, 1);
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
        float poisonLinePosition = (1f- (effect.GetCurrentPoisonAmount() / maxHP + (1 - currentHPBar.fillAmount))) * width;
        poisonLine.rectTransform.anchoredPosition = new Vector2(poisonLinePosition, 0f);

        if (poisonLinePosition <= 0)
        {
            currentHPBar.color = effect.data.color;
        }
        else
        {
            currentHPBar.color = backupColor;
        }
    }

}
