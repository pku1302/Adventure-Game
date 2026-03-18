using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationComponent : MonoBehaviour
{
    public Animator animator;
    public Vector2 lastDirection;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetMove(Vector2 direction)
    {
        lastDirection = direction;

        animator.SetTrigger("Move");
        animator.SetFloat("Move X", direction.x);
        animator.SetFloat("Move Y", direction.y);
    }

    public void SetIdle()
    {
        animator.SetTrigger("Idle");
        animator.SetFloat("Move X", lastDirection.x);
        animator.SetFloat("Move Y", lastDirection.y);
    }

    public void SetDead()
    {
        animator.SetFloat("Move X", lastDirection.x);
        animator.SetTrigger("Die");
    }

    public void SetCreateFood()
    {
        animator.SetFloat("Move X", lastDirection.x);
        animator.SetFloat("Move Y", lastDirection.y);
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Create Food"))
        {
            animator.SetTrigger("Create Food");
        }
    }

    public void SetCreateFoodDone()
    {
        animator.SetTrigger("Create Food Done");
    }

    public void SetAttack(Vector2 direction)
    {
        animator.SetTrigger("Attack");
        animator.SetFloat("Move X", direction.x);
        animator.SetFloat("Move Y", direction.y);
    }

    public void SetStop(Vector2 direction)
    {
        animator.SetTrigger("Stop");
        animator.SetFloat("Move X", direction.x);
        animator.SetFloat("Move Y", direction.y);
    }

    public void SetHit(Vector2 direction)
    {
        animator.SetTrigger("Hit");
        animator.SetFloat("Move X", direction.x);
        animator.SetFloat("Move Y", direction.y);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
