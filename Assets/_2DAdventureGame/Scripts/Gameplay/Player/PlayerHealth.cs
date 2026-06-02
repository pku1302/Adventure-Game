using System;
using UnityEngine;
using System.Collections;
public enum DamageType
{
    Normal,
    Poison
}

public class PlayerHealth : MonoBehaviour
{
    private PlayerStats stats;
    public float maxHP;
    public float currentHP;
    public event Action<float, float> OnHPChanged;
    public event Action OnDeath;
    public static event Action<PlayerHealth> OnSpawned;
    public bool isDead { get; private set; }
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] normalHitSFXs;
    [SerializeField]
    private AudioClip deathSFX;

    void Start()
    {
        OnSpawned?.Invoke(this);
    }

    public void Init(PlayerStats stats)
    {
        this.stats = stats;
        currentHP = stats.maxHP;
        maxHP = stats.maxHP;
        OnHPChanged?.Invoke(currentHP, maxHP);
    }

    public void TakeDamage(float damage, DamageType type)
    {
        if (isDead)
        {
            return;
        }

        float reduction = stats.totalDefense;
        float finalDamage = damage;
        if (type == DamageType.Normal)
        {
            finalDamage -= reduction;
        }
        currentHP -= finalDamage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        OnHPChanged?.Invoke(currentHP, maxHP);

        if (type == DamageType.Normal)
        {
            AudioClip clip = normalHitSFXs[UnityEngine.Random.Range(0, normalHitSFXs.Length)];
            StartCoroutine(HitShake(0.1f, 0.2f));
            audioSource.PlayOneShot(clip);
        }

        if (currentHP <= 0)
        {
            Die();
        }
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

    public void TakeHeal(float amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        OnHPChanged?.Invoke(currentHP, maxHP);
    }

    private void Die()
    {
        isDead = true;
        audioSource.PlayOneShot(deathSFX);
        OnDeath?.Invoke();
    }
}
