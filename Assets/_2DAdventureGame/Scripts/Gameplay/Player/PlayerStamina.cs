using System;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public float currentStamina;
    public float maxStamina;
    
    private float recoveryRate = 30f;
    private float recoveryTimer = 0f;
    private float RecoveryTime = 3f;

    public StaminaState state = StaminaState.Normal;
    public event Action OnExhausted;
    public event Action OnNormal;

    public enum StaminaState
    {
        Normal,
        Exhausted
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentStamina = PlayerStats.stats.maxStamina;
        maxStamina = PlayerStats.stats.maxStamina;
    }

    public bool isExhausted()
    {
        return state == StaminaState.Exhausted;
    }

    private void Update()
    {
        RecoverStamina();

        if (recoveryTimer > 0f)
        {
            recoveryTimer -= Time.deltaTime;    
        }
    }

    public bool TryUseStamina(float amount)
    {
        if (state == StaminaState.Exhausted)
            return false;

        if (currentStamina < amount)
        {
            EnterExhaustion();
            return true;
        }

        currentStamina -= amount;
        recoveryTimer = RecoveryTime;

        return true;
    }

    private void EnterExhaustion()
    {
        state = StaminaState.Exhausted;
        OnExhausted?.Invoke();
        currentStamina = 0f;
    }

    public void RecoverStamina()
    {
        if (currentStamina < maxStamina && 
            recoveryTimer <= 0f)
        {
            currentStamina += recoveryRate * Time.deltaTime;
            if (currentStamina > maxStamina)
                currentStamina = maxStamina;
        }

        if (state == StaminaState.Exhausted && 
            currentStamina >= maxStamina)
        {
            state = StaminaState.Normal;
            OnNormal?.Invoke();
        }
    }
}
