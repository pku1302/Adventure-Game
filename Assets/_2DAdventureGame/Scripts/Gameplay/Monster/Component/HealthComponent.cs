using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    private Monster monster;
    private AIComponent ai;

    public int CurrentHp {  get; private set; }
    public int MaxHp { get; private set; }
    public bool IsDead => CurrentHp <= 0;

    public event Action OnHit;
    public event Action OnDeath;
    public event Action OnGuard;

    private void Awake()
    {
        monster = GetComponent<Monster>();
        ai = GetComponent<AIComponent>();
        MaxHp = monster.Data.maxHp;
        CurrentHp = MaxHp;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void TakeDamage(int amount, bool isBlocked)
    {
        if (IsDead) return;

        if (isBlocked)
        {
            OnGuard?.Invoke();
            return;
        }

        CurrentHp -= amount;
        OnHit?.Invoke();

        if (CurrentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        CurrentHp = 0;
        OnDeath?.Invoke();
    }
}
