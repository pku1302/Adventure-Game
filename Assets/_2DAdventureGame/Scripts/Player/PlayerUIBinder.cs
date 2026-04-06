using UnityEngine;

public class PlayerUIBinder : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public StatusEffectManager playerBuff;
    public HPBar hpBar;

    void Start()
    {
        playerHealth.OnHPChanged += hpBar.UpdateHP;
        playerBuff.OnHealEffectAdded += hpBar.UpdateGenerationHP;
    }
}
