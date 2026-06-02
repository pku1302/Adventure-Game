using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public float damage = 5f;
    private bool hasHit = false;
    public StatusEffectData poisonData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit) return;

        if (collision.CompareTag("Player"))
        {
            var health = collision.GetComponent<PlayerHealth>();
            var statusManager = collision.GetComponent<StatusEffectManager>();
            if (health != null)
            {
                health.TakeDamage(damage, DamageType.Normal);
                hasHit = true;
            }
            if (statusManager != null)
            {
                statusManager.AddEffect(new PoisonDebuff(poisonData));
            }
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
