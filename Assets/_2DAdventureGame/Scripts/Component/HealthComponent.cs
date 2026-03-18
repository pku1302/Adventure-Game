using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    private Monster monster;
    private AIComponent ai;

    public int CurrentHp {  get; private set; }
    public int MaxHp { get; private set; }

    public bool IsDead => CurrentHp <= 0;

    public event Action OnDamaged;
    public event Action OnDeath;

    private void Awake()
    {
        monster = GetComponent<Monster>();
        ai = GetComponent<AIComponent>();
        MaxHp = monster.Data.maxHp;
        CurrentHp = MaxHp;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void TakeDamage(int amount)
    {
        if (IsDead) return;

        CurrentHp -= amount;

        if (CurrentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        CurrentHp = 0;
        OnDeath?.Invoke();
        ai.Animation.SetDead();
    }
}
