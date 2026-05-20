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

    private float runningCost = 8.0f;

    private Animator animator;
    private Vector2 moveDirection = new Vector2(1, 0);
    private Vector2 mouseDirection;
    private PlayerStats stats;
    private StatusEffectManager statusEffectManager;
    private InteractionController interactionController;
    private ProgressController progressController;
    private PlayerStamina stamina;
    private GameManager gameManager;
    private PlayerItem playerItem;

    [Header("Player")]
    public InputAction MoveAction; // WASD
    public InputAction LaunchAction; // Mouse Left
    public InputAction RollAction; // Space
    public InputAction InteractionAction; // F
    public InputAction RunAction; // Shift
    public InputAction MouseRightAction; // Mouse Right

    public bool IsSnared { get; private set; }
    public bool IsRunning { get; private set; }
    public bool IsInteracting { get; private set; }

    public Vector2 move { get; private set; }

    private bool isKnockback = false;

    public void Init(InteractionController itc, PlayerItem playerItem, ProgressController progressController, GameManager gameManager)
    {
        interactionController = itc;
        this.playerItem = playerItem;
        this.progressController = progressController;
        this.gameManager = gameManager;
    }

    void Start()
    {
        MoveAction.Enable();
        LaunchAction.Enable();
        RollAction.Enable();
        InteractionAction.Enable();
        RunAction.Enable();
        MouseRightAction.Enable();

        stats = GetComponent<PlayerStats>();
        dash = GetComponent<PlayerDash>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        statusEffectManager = GetComponent<StatusEffectManager>();
        stamina = GetComponent<PlayerStamina>();
        dash.OnDashStart += () => animator.SetTrigger("Roll");

        IsRunning = false;
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

        move = MoveAction.ReadValue<Vector2>();

        if (RunAction.IsPressed())
        {
            IsRunning = true;
        }

        if (RunAction.WasReleasedThisFrame())
        {
            IsRunning = false;
        }

        if (!EventSystem.current.IsPointerOverGameObject() && LaunchAction.WasPressedThisFrame())
        {
            Launch();
            playerItem.CancelUse();
        }

        if (RollAction.WasPressedThisFrame())
        {
            dash.TryDash();
            playerItem.CancelUse();
        }

        progressController.Tick();

        if (InteractionAction.IsPressed())
        {
            IInteractable target = interactionController.BeginInteract();
            IsInteracting = true;
        }

        if (InteractionAction.WasReleasedThisFrame())
        {
            interactionController.CancleInteract();
            IsInteracting = false;
        }

        if (MouseRightAction.WasPressedThisFrame() && playerItem.isUsing)
        {
            playerItem.CancelUse();
        }
    }
    private void FixedUpdate()
    {
        if (dash.IsDashing)
        {
            Roll();
        }
        Move(move);
    }

    public bool IsGamePlay()
    {
        return gameManager.CurrentState == GameState.GamePlay;
    }

    public void Roll()
    {
        float dashSpeed = dash.GetDashSpeed();
        Vector2 position = (Vector2)rigidbody2d.position + mouseDirection * dashSpeed * Time.fixedDeltaTime;
        rigidbody2d.MovePosition(position);
    }

    public void ToggleIsSnared()
    {
        IsSnared = !IsSnared;
    }


    public void Move(Vector2 move)
    {
        if (dash.IsDashing || isKnockback || IsSnared || IsInteracting || gameManager.CurrentState != GameState.GamePlay)
        {
            return;
        }
        float speed = stats.baseMoveSpeed;

        if (IsRunning &&
            !playerItem.isUsing &&
            !stamina.isExhausted() &&
            move != Vector2.zero)
        {
            stamina.TryUseStamina(runningCost * Time.deltaTime);
            speed *= 2.5f;
        }

        speed = statusEffectManager.GetFinalSpeed(speed);
        if (playerItem.isUsing)
        {
            speed *= 0.5f;
        }

        Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.fixedDeltaTime;
        rigidbody2d.MovePosition(position);
    }

    public void SetAnimation()
    {
        if (IsInteracting || gameManager.CurrentState != GameState.GamePlay)
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
        if (IsInteracting || gameManager.CurrentState != GameState.GamePlay)
        {
            return;
        }
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.1f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(mouseDirection, 300);
        animator.SetTrigger("Launch");
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
