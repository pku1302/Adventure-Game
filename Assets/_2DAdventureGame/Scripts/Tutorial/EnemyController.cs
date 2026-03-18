using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f;
    public bool isBroken { get { return broken; } }
    public ParticleSystem smokeParticleEffect;

    private Rigidbody2D rigidbody2d;
    private float timer;
    private int direction = 1;
    private Animator animator;
    private bool broken = true;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }


    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    private void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }

        Vector2 position = (Vector2)rigidbody2d.position;

        if(vertical)
        {
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);

            position.y = position.y + speed * direction * Time.deltaTime;
        }
        else
        {
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
            position.x = position.x + speed * direction * Time.deltaTime;
        }

        rigidbody2d.MovePosition(position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController controller = collision.gameObject.GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        broken = false;
        rigidbody2d.simulated = false;
        smokeParticleEffect.Stop();
    }
}
