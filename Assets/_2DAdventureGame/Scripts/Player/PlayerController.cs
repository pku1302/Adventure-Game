using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 5;
    public float speed = 3.0f;
    public float dashSpeed = 6.0f;
    public float dashCoolTime = 1.0f;
    public float dashTime = 0.4f;

    private float dashCoolTimer = 0.0f;
    private float dashTimer = 0.0f;
    private bool isDashing = false;

    public GameObject projectilePrefab;
    public int health { get { return currentHealth; } }
    public float timeInvincible = 2.0f;
    public InputAction MoveAction;
    public InputAction LaunchAction;
    public InputAction TalkAction;
    public InputAction RollAction;

    private int currentHealth;
    private Rigidbody2D rigidbody2d;
    private Vector2 move;
    private bool isInvincible;
    private float damageCooldown;
    private Animator animator;
    private Vector2 moveDirection = new Vector2(1, 0);
    private AudioSource audioSource;
    public AudioClip[] hitSFXs;
    private int currentDirection = 0;
    private PlayerDash dash;
    private Vector2 mouseDirection;

    public static PlayerController player;

    void Start()
    {
        player = this;

        MoveAction.Enable();
        LaunchAction.Enable();
        TalkAction.Enable();
        RollAction.Enable();

        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        dash = GetComponent<PlayerDash>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        if(!isDashing)
            mouseDirection = worldPos - transform.position;

        move = MoveAction.ReadValue<Vector2>();
        RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, moveDirection, 1.5f, LayerMask.GetMask("NPC"));

        if (hit.collider != null)
        {
            FindFriend(hit);
        }

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }

        animator.SetFloat("Look X", mouseDirection.x);
        animator.SetFloat("Look Y", mouseDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }

        if (LaunchAction.WasPressedThisFrame())
        {
            Launch();
        }

        if (RollAction.WasPressedThisFrame() && dashCoolTimer <= 0.0f)
        {
            animator.SetTrigger("Roll");
            dashCoolTimer = dashCoolTime;
            dashTimer = dashTime;
            isDashing = true;
            MoveAction.Disable();
            LaunchAction.Disable();
        }

        if (dashCoolTimer > 0.0f)
            dashCoolTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.fixedDeltaTime;
        Vector2 mDirection = mouseDirection.normalized;
        if (isDashing)
        {
            dashTimer -= Time.fixedDeltaTime;
            position = (Vector2)rigidbody2d.position + mDirection * dashSpeed * Time.fixedDeltaTime;
            rigidbody2d.MovePosition(position);

            if (dashTimer <= 0.0f)
            {
                isDashing = false;
                MoveAction.Enable();
                LaunchAction.Enable();
            }
        }
        else
        {
            rigidbody2d.MovePosition(position);
        }
    }

    void FindFriend(RaycastHit2D hit)
    {
        if (TalkAction.WasPressedThisFrame())
        {
            UIHandler.instance.DisplayDialogue();
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            AudioClip clip = hitSFXs[Random.Range(0, hitSFXs.Length)];
            audioSource.PlayOneShot(clip);
            isInvincible = true;
            damageCooldown = timeInvincible;
            animator.SetTrigger("Hit");
            StartCoroutine(HitShake(0.1f, 0.2f));
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
    }

    private void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.1f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(mouseDirection, 300);
        animator.SetTrigger("Launch");
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public IEnumerator HitShake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
