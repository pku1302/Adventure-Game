using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float maxDistance = 5.0f;
    private AudioSource audioSource;
    public AudioClip[] hitSFXs;
    public AudioClip criticalSFX;
    private Vector3 startPosition;
    private int damage;

    private Rigidbody2D rigidbody2d;
    private bool isLastShot = false;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        startPosition = transform.position;
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        float distance = Vector3.Distance(startPosition, transform.position);

        if (distance >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    public void Init(Vector2 direction, int damage, bool isLastShot)
    {
        rigidbody2d.AddForce(direction * speed);
        this.damage = damage;
        this.isLastShot = isLastShot;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AIComponent enemy = collision.GetComponent<AIComponent>();

        if (enemy != null)
        {
            if (enemy is JiangshiAIComponent jiangshi && jiangshi.guardStack > 0)
            {
                jiangshi.guardStack -= 1;
                jiangshi.Health.TakeDamage(0, true, false);
            }
            else
            {
                enemy.Health.TakeDamage(damage, false, isLastShot);
            }
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
