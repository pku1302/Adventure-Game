using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 5;
    public float speed = 3.0f;
    public float dashSpeed = 6.0f;

    public GameObject projectilePrefab;
    public int health { get { return currentHealth; } }
    public float timeInvincible = 2.0f;

    private int currentHealth;
    private Rigidbody2D rigidbody2d;
    private bool isInvincible;
    private bool isDashing = false;
    private bool isLooting = false;
    private float damageCooldown;
    private Animator animator;
    private Vector2 moveDirection = new Vector2(1, 0);
    private AudioSource audioSource;
    public AudioClip[] hitSFXs;
    private Vector2 mouseDirection;

    public static PlayerController player;

    void Start()
    {
        player = this;
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        if (!isDashing)
            mouseDirection = worldPos - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, moveDirection, 1.5f, LayerMask.GetMask("NPC"));

        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }
    }
    private void FixedUpdate()
    {
        if (isDashing)
        {
            M_Roll();
        }
    }

    public void Roll(Vector2 move)
    {
        if (isDashing || isLooting)
        {
            return;
        }
        animator.SetTrigger("Roll");
        isDashing = true;
    }

    public void Loot()
    {
        isLooting = true;
    }

    public void EndLooting()
    {
        isLooting = false;
    }

    public void EndDash()
    {
        isDashing = false;
    }

    private void M_Roll()
    {
        Vector2 mDirection = mouseDirection.normalized;
        isDashing = true;

        Vector2 position = (Vector2)rigidbody2d.position + mDirection * dashSpeed * Time.fixedDeltaTime;
        rigidbody2d.MovePosition(position);
    }

    public void Move(Vector2 move)
    {
        if (isDashing || isLooting)
        {
            return;
        }

        Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.fixedDeltaTime;
        rigidbody2d.MovePosition(position);
    }

    public void SetAnimation(Vector2 move)
    {
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
    }

    public void Launch()
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
