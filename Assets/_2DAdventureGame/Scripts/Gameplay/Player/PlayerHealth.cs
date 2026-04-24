using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHP;
    public float currentHP;
    public event Action<float, float> OnHPChanged;
    public event Action OnDeath;
    public static event Action<PlayerHealth> OnSpawned;

    private PlayerStats stats;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
    }

    void Start()
    {
        OnSpawned?.Invoke(this);
        currentHP = stats.maxHP;
        maxHP = stats.maxHP;
        OnHPChanged?.Invoke(currentHP, maxHP);
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        OnHPChanged?.Invoke(currentHP, maxHP);
        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void TakeHeal(float amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        OnHPChanged?.Invoke(currentHP, maxHP);
    }

    private void Die()
    {
        OnDeath?.Invoke();
    }
}
