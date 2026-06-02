using UnityEngine;

public class PlayerStats
{
    public float baseMoveSpeed = 1.0f;
    public float baseRunningSpeed = 2.0f;
    public float maxHP = 100f;
    public float baseMaxStamina = 100f;
    public float attackSpeed = 1f;
    public float baseAttack = 10f;
    public float totalAttack;
    public float baseDefense = 0f;
    public float totalDefense;
    public float totalMoveSpeed;
    public float totalMaxStamina;

    private PlayerStamina playerStamina;

    public PlayerStats()
    {
        totalAttack = baseAttack;
        totalDefense = baseDefense;
        totalMoveSpeed = baseMoveSpeed;
        totalMaxStamina = baseMaxStamina;
    }

    public void Init(PlayerStamina playerStamina)
    {
        this.playerStamina = playerStamina;
    }

    public void RecalculateStamina(float amount)
    {
        totalMaxStamina = baseMaxStamina;
        totalMaxStamina += amount;
        playerStamina.SetMaxStamina(totalMaxStamina);
    }

    public void GainSpeedBuff(float amount)
    {
        totalMoveSpeed += amount;
    }

    public void CancelSpeedBuff(float amount)
    {
        totalMoveSpeed -= amount;
    }
}
