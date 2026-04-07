using UnityEngine;
using UnityEngine.UI;

public class DebuffUI : MonoBehaviour
{
    public Image gaugeImage;
    private StatusEffect effect;

    public void Init(StatusEffect se)
    {
        effect = se;
        gaugeImage.color = effect.data.color;
        gaugeImage.fillAmount = Mathf.Clamp01((float)effect.stack / effect.data.maxStack);
    }

    private void Update()
    {
        if (effect.IsActivated)
        {
            SetActiveGauge();
            gaugeImage.fillAmount = Mathf.Clamp01(1 - effect.elapsed / effect.data.duration);
        }

        if (effect.IsFinished)
        {
            DestoryGauge();
        }
    }

    public void SetUI(int i)
    {
        if (!effect.IsActivated)
        {
            gaugeImage.fillAmount = Mathf.Clamp01((float)effect.stack / effect.data.maxStack);
        }
    }

    private void SetActiveGauge()
    {
        gaugeImage.color = effect.data.color * 1.5f;
    }

    private void DestoryGauge()
    {
        Destroy(gameObject);
    }
}
