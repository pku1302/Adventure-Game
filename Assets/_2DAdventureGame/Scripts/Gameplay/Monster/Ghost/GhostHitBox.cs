using UnityEngine;

public class GhostHitBox : MonoBehaviour
{
    public float damage = 30f;
    private bool hasHit = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit) return;

        if (collision.CompareTag("Player"))
        {
            var health = collision.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damage, DamageType.Normal);
                hasHit = true;
            }
        }
    }

}
