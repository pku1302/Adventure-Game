using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    private Monster monster;
    private Rigidbody2D rb;
    [SerializeField]
    private LayerMask wallLayer;
    private AnimationComponent animationComponent;

    private Vector2 avoidDir;
    private float avoidTimer = 0f;
    private float avoidTime = 0.3f;

    private void Awake()
    {
        monster = GetComponent<Monster>();
        rb = GetComponent<Rigidbody2D>();
        animationComponent = GetComponent<AnimationComponent>();
    }

    // 슬로우 디버프까지 계산한 총 이속을 리턴할 것임
    public float GetMoveSpeed()
    {
        return rb.linearVelocity.magnitude;
    }

    public void StopMonster()
    {
        rb.linearVelocity = Vector2.zero;
    }

    public bool Move(Transform target, float speed)
    {
        if (target == null) return false;

        return Move((Vector2)target.position, speed);
    }

    public bool Move(Vector2 target, float speed)
    {
        Vector2 currentPosition = rb.position;
        Vector2 direction = (target - currentPosition).normalized;

        if (avoidTimer > 0)
        {
            avoidTimer -= Time.fixedDeltaTime;
            rb.linearVelocity = avoidDir * speed;
            return true;
        }

        RaycastHit2D hit = Physics2D.Raycast(rb.position, direction, 0.1f, wallLayer);

        if (hit.collider != null)
        {
            avoidDir = Vector2.Perpendicular(direction);
            avoidTimer = avoidTime;
            direction = avoidDir;
        }
        rb.linearVelocity = direction * speed;

        return true;
    }

    public void Rush(Vector2 direction)
    {
        rb.linearVelocity = direction;
    }

}
