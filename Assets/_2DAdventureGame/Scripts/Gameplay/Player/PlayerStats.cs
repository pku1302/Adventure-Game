using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float baseMoveSpeed = 2.0f;
    public float baseRunningSpeed = 4.0f;
    public float maxHP = 100f;
    public float maxStamina = 100f;

    public static PlayerStats stats;

    private void Awake()
    {
        stats = this;
    }
}
