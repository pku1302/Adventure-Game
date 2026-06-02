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
    
    public float speed {  get; private set; }

    private void Awake()
    {
        monster = GetComponent<Monster>();
        rb = GetComponent<Rigidbody2D>();
        animationComponent = GetComponent<AnimationComponent>();
        speed = monster.Data.moveSpeed;
    }

    // 슬로우 디버프까지 계산한 총 이속을 리턴할 것임
    public float GetMoveSpeed()
    {
        return rb.linearVelocity.magnitude;
    }

    public void IncreaseMoveSpeed(float amount)
    {
        speed += amount;
    }

    public void ResetMoveSpeed()
    {
        speed = monster.Data.moveSpeed;
    }

    public void SetMoveSpeed(float amount)
    {
        speed = amount;
    }

    public void StopMonster()
    {
        rb.linearVelocity = Vector2.zero;
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
            rb.linearVelocity = avoidDir * speed;
            return true;
        }

        RaycastHit2D hit = Physics2D.Raycast(rb.position, direction, 1f, wallLayer);
        bool wrongWayFlag = false;

        if (hit.collider != null)
        {
            avoidDir = -direction;
            avoidTimer = avoidTime;
            direction = avoidDir;
            wrongWayFlag = true;
        }

        rb.linearVelocity = direction * speed;
        if (wrongWayFlag)
        {
            return false;
        }
        return true;
    }

    public void Rush(Vector2 direction)
    {
        rb.linearVelocity = direction;
    }

}
