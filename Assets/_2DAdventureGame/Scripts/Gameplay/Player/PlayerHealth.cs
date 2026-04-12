using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHP;
    public float currentHP;
    public event Action<float, float> OnHPChanged;

    private PlayerStats stats;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
    }

    void Start()
    {
        currentHP = stats.maxHP;
        maxHP = stats.maxHP;
        OnHPChanged?.Invoke(currentHP, maxHP);
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        OnHPChanged?.Invoke(currentHP, maxHP);
    }

    public void TakeHeal(float amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        OnHPChanged?.Invoke(currentHP, maxHP);
    }
}
