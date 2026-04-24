using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float timeInvincible = 2.0f;

    private Rigidbody2D rigidbody2d;
    private PlayerDash dash;
    private bool isLooting = false;
    private bool isKnockback = false;
    private float runningCost = 8.0f;

    private Animator animator;
    private Vector2 moveDirection = new Vector2(1, 0);
    private Vector2 mouseDirection;
    private PlayerStats stats;
    private StatusEffectManager statusEffectManager;
    private PlayerStamina stamina;

    public InputManager inputManager;
    public bool IsSnared {  get; private set; }

    void Start()
    {
        stats= GetComponent<PlayerStats>();
        dash = GetComponent<PlayerDash>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        statusEffectManager = GetComponent<StatusEffectManager>();
        stamina = GetComponent<PlayerStamina>();
        dash.OnDashStart += () => animator.SetTrigger("Roll");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f;
        if (!dash.IsDashing)
            mouseDirection = (Vector2)(worldPos - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, moveDirection, 1.5f, LayerMask.GetMask("NPC"));
        SetAnimation();

        if (InputManager.Instance.WasLaunchActionPressed)
        {
            Launch();
        }

        if (InputManager.Instance.WasRollActionPressed)
        {
            dash.TryDash();
        }
    }
    private void FixedUpdate()
    {
        if (dash.IsDashing)
        {
            Roll();
        }
        Move(InputManager.Instance.move);
    }

    public void Roll()
    {
        float dashSpeed = dash.GetDashSpeed();
        Vector2 position = (Vector2)rigidbody2d.position + mouseDirection * dashSpeed * Time.fixedDeltaTime;
        rigidbody2d.MovePosition(position);
    }

    public void LootStart()
    {
        isLooting = true;
    }

    public void LootEnd()
    {
        isLooting = false;
    }

    public void ToggleSnare()
    {
        IsSnared = !IsSnared;
    }

    public void Move(Vector2 move)
    {
        if (dash.IsDashing || isLooting || isKnockback || IsSnared)
        {
            return;
        }
        float speed = stats.baseMoveSpeed;

        if (inputManager.IsRunning && 
            !statusEffectManager.isUsingItem &&
            !stamina.isExhausted() &&
            move != Vector2.zero )
        {
            stamina.TryUseStamina(runningCost * Time.deltaTime);
            speed *= 2.5f;

        }

        speed = statusEffectManager.GetFinalSpeed(speed);

        Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.fixedDeltaTime;
        rigidbody2d.MovePosition(position);
    }

    public void SetAnimation()
    {
        Vector2 move = InputManager.Instance.move;
        if (isLooting)
        {
            return;
        }
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }

        animator.SetFloat("Look X", mouseDirection.x);
        animator.SetFloat("Look Y", mouseDirection.y);
        animator.SetFloat("Speed", move.magnitude);
    }

    public void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.1f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(mouseDirection, 300);
        animator.SetTrigger("Launch");
    }

    public IEnumerator HitShake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = UnityEngine.Random.Range(-1f, 1f) * magnitude;
            float y = UnityEngine.Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }

    public void ApplyKnockback(Vector2 dir, float force, float duration)
    {
        StartCoroutine(Knockback(dir, force, duration));
    }

    IEnumerator Knockback(Vector2 dir, float force, float duration)
    {
        isKnockback = true;

        rigidbody2d.linearVelocity = Vector2.zero;
        rigidbody2d.AddForce(dir * force, ForceMode2D.Impulse);
        if (dash.IsDashing)
            dash.EndDash();

        yield return new WaitForSeconds(duration);

        isKnockback = false;
    }
}
