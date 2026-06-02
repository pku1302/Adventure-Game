using System;
using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public bool IsDashing { get; private set; }
    public Vector2 DashDirection { get; private set; }
    public float dashCost = 10f;
    public event Action OnDashStart;

    private float dashSpeed = 6.0f;
    private PlayerStamina stamina;
    private PlayerController player;

    private void Awake()
    {
        IsDashing = false;
        player = GetComponent<PlayerController>();
        stamina = GetComponent<PlayerStamina>();
    }

    public float GetDashSpeed()
    {
        return dashSpeed;
    }

    public void TryDash()
    {
        if (IsDashing || player.IsSnared || !player.IsGamePlay() || stamina.isExhausted() || player.IsDead) return;

        if (stamina.TryUseStamina(dashCost))
        {
            IsDashing = true;
            OnDashStart?.Invoke();
        }
    }

    public void EndDash()
    {
        IsDashing = false;
        if (player.ReservedLaunch)
        {
            player.Launch();
            player.SetReservedLaunch();
        }
    }
}
