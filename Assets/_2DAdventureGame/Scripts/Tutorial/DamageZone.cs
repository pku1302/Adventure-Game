using UnityEngine;

public class DamageZone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController controller = collision.GetComponent<PlayerController>();

        if (controller != null )
        {
            controller.ChangeHealth(-1);
        }
    }
}
