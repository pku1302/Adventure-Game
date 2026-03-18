using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    Rigidbody2D rb;
    public float dashSpeed = 12f;
    public float dashTime = 0.2f;
    public float dashCooldown = 2f;

    bool isDashing;
    bool canDash = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Roll(Vector2 direction)
    {
        if (!isDashing && canDash)
            StartCoroutine(Dash(direction));
    }

    IEnumerator Dash(Vector2 direction)
    {
        isDashing = true;

        rb.linearVelocity = direction * dashSpeed;

        yield return new WaitForSeconds(dashTime);

        isDashing = false;
        canDash = false;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }
}
