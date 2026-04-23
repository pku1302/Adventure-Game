using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private Vector2 m_direction;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 100.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        m_direction = direction;
        rigidbody2d.AddForce(direction * force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AIComponent enemy = collision.GetComponent<AIComponent>();


        if (enemy != null)
        {
            if (enemy is JiangshiAIComponent jiangshi && jiangshi.guardStack > 0)
            {
                jiangshi.guardStack -= 1;
                jiangshi.Health.TakeDamage(0, true);
            }
            else
            {
                enemy.Health.TakeDamage(10, false);
            }
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
