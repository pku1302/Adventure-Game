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
    private Vector2 rollDirection;
    private StatusEffectManager statusEffectManager;
    private InteractionController interactionController;
    private ProgressController progressController;
    private ProgressController reloadProgressController;
    private PlayerStamina stamina;
    private GameManager gameManager;
    private PlayerItem playerItem;
    private PlayerWeapon weapon;
    private AudioSource audioSource;
    private PlayerHealth health;

    [Header("Player")]
    public InputAction MoveAction; // WASD
    public InputAction LaunchAction; // Mouse Left
    public InputAction RollAction; // Space
    public InputAction InteractionAction; // F
    public InputAction RunAction; // Shift
    public InputAction MouseRightAction; // Mouse Right

    public AudioClip snareSFX;

    public bool IsSnared { get; private set; }
    public bool IsRunning { get; private set; }
    public bool IsInteracting { get; private set; }
    public bool IsDead { get; private set; }
    public bool ReservedLaunch { get; private set; }
    public PlayerStats stats { get; private set; }

    public Vector2 move { get; private set; }

    private bool isKnockback = false;

    public void Init(InteractionController itc, PlayerItem playerItem, ProgressController progressController, GameManager gameManager, PlayerStats stats, ProgressController reloadProgressController)
    {
        interactionController = itc;
        this.playerItem = playerItem;
        this.progressController = progressController;
        this.reloadProgressController = reloadProgressController;
        this.gameManager = gameManager;
        this.stats = stats;
    }

    private void OnDestroy()
    {
        if (progressController != null)
        {
            progressController.Cancel();
        }
    }

    void Awake()
    {
        MoveAction.Enable();
        LaunchAction.Enable();
        RollAction.Enable();
        InteractionAction.Enable();
        RunAction.Enable();
        MouseRightAction.Enable();

        weapon = GetComponent<PlayerWeapon>();
        dash = GetComponent<PlayerDash>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        statusEffectManager = GetComponent<StatusEffectManager>();
        stamina = GetComponent<PlayerStamina>();
        audioSource = GetComponent<AudioSource>();
        health = GetComponent<PlayerHealth>();
        dash.OnDashStart += () => animator.SetTrigger("Roll");

        health.OnDeath += Dead;
        IsRunning = false;
        ReservedLaunch = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f;
        mouseDirection = (Vector2)(worldPos - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, moveDirection, 1.5f, LayerMask.GetMask("NPC"));
        SetAnimation();

        if (IsDead)
        {
            return;
        }

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
            rollDirection = mouseDirection;
            dash.TryDash();
            playerItem.CancelUse();
        }

        progressController.Tick();
        reloadProgressController.Tick();

        if (InteractionAction.IsPressed())
        {
            IInteractable target = interactionController.BeginInteract();
            if (target != null)
            {
                IsInteracting = true;
            }
        }

        if (IsInteracting && InteractionAction.WasReleasedThisFrame())
        {
            interactionController.CancelInteract();
            IsInteracting = false;
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

    private void Dead()
    {
        IsDead = true;
        animator.SetTrigger("Dead");
    }

    public bool IsGamePlay()
    {
        return gameManager.CurrentState == GameState.GamePlay;
    }

    public void Roll()
    {
        float dashSpeed = dash.GetDashSpeed();
        Vector2 position = (Vector2)rigidbody2d.position + rollDirection * dashSpeed * Time.fixedDeltaTime;
        rigidbody2d.MovePosition(position);
        weapon.SetCoolTime();
    }

    public void ToggleIsSnared()
    {
        IsSnared = !IsSnared;
        if (IsSnared)
        {
            audioSource.PlayOneShot(snareSFX);
        }
    }


    public void Move(Vector2 move)
    {
        if (dash.IsDashing || isKnockback || IsSnared || IsInteracting || gameManager.CurrentState != GameState.GamePlay || IsDead)
        {
            return;
        }
        float speed = stats.totalMoveSpeed;

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
        if (IsInteracting || gameManager.CurrentState != GameState.GamePlay || stamina.isExhausted() || IsSnared || IsDead)
        {
            return;
        }

        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        float progress = info.normalizedTime;

        if (dash.IsDashing)
        {
            if (progress >= 0.5)
            {
                ReservedLaunch = true;
            }
            return;
        }

        weapon.Fire(mouseDirection);
        animator.SetTrigger("Launch");
    }

    public void SetReservedLaunch()
    {
        ReservedLaunch = false;
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

        rigidbody2d.linearVelocity =
            Vector2.zero;

        isKnockback = false;
    }
}
