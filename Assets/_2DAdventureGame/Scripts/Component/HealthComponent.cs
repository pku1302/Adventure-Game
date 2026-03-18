using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    private Monster monster;
    private AIComponent ai;

    public int CurrentHp {  get; private set; }
    public int MaxHp { get; private set; }
    public bool IsDead => CurrentHp <= 0;

    public event Action<Vector2> OnHit;
    public event Action OnDeath;

    private void Awake()
    {
        monster = GetComponent<Monster>();
        ai = GetComponent<AIComponent>();
        MaxHp = monster.Data.maxHp;
        CurrentHp = MaxHp;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void TakeDamage(int amount, Vector2 direction)
    {
        if (IsDead) return;

        CurrentHp -= amount;
        OnHit?.Invoke(direction);

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
