using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    private Monster monster;
    private Rigidbody2D rb;
    [SerializeField]
    private LayerMask wallLayer;
    private AnimationComponent animationComponent;
    public float currentMoveSpeed;

    private Vector2 avoidDir;
    private float avoidTimer = 0f;
    private float avoidTime = 0.3f;

    private void Awake()
    {
        monster = GetComponent<Monster>();
        rb = GetComponent<Rigidbody2D>();
        animationComponent = GetComponent<AnimationComponent>();
        currentMoveSpeed = monster.Data.moveSpeed;
    }

    // 슬로우 디버프까지 계산한 총 이속을 리턴할 것임
    public float GetMoveSpeed()
    {
        return monster.Data.moveSpeed;
    }

    public void StopMonster()
    {
        rb.linearVelocity = Vector2.zero;
        animationComponent.animator.SetFloat("Speed", 0);
    }

    public bool Move(Transform target)
    {
        if (target == null) return false;

        return Move((Vector2)target.position);
    }

    public bool Move(Vector2 target)
    {
        Vector2 currentPosition = rb.position;
        Vector2 direction = (target - currentPosition).normalized;

        if (avoidTimer > 0)
        {
            avoidTimer -= Time.fixedDeltaTime;
            rb.linearVelocity = avoidDir * currentMoveSpeed;
            return true;
        }

        RaycastHit2D hit = Physics2D.Raycast(rb.position, direction, 0.1f, wallLayer);

        if (hit.collider != null)
        {
            avoidDir = Vector2.Perpendicular(direction);
            avoidTimer = avoidTime;
            direction = avoidDir;
        }
        animationComponent.SetMove(direction);
        animationComponent.animator.SetFloat("Speed", currentMoveSpeed);
        rb.linearVelocity = direction * currentMoveSpeed;

        return true;
    }
}
