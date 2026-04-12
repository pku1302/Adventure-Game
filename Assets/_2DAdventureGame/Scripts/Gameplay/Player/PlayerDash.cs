using System;
using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public bool IsDashing { get; private set; }
    public Vector2 DashDirection { get; private set; }
    public float dashCost = 10f;

    private float dashSpeed = 6.0f;
    private PlayerStamina stamina;
    public event Action OnDashStart;

    private void Awake()
    {
        IsDashing = false;
        stamina = GetComponent<PlayerStamina>();
    }

    public float GetDashSpeed()
    {
        return dashSpeed;
    }

    public void TryDash()
    {
        if (IsDashing) return;

        if (stamina.TryUseStamina(dashCost))
        {
            IsDashing = true;
            OnDashStart?.Invoke();
        }
    }

    public void EndDash()
    {
        IsDashing = false;
    }
}
