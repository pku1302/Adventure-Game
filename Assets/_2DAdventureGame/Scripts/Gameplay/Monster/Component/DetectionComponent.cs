using System;
using UnityEngine;

public class DetectionComponent : MonoBehaviour
{
    public AIComponent ai;
    public event Action<Transform> OnTargetEnter;
    private CircleCollider2D trigger;

    void Start()
    {
        trigger = GetComponent<CircleCollider2D>();
        trigger.radius = 100f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnTargetEnter?.Invoke(collision.transform);
            Destroy(gameObject);
        }
    }
}
