using System;
using UnityEngine;

public class GhostSnare : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction;
    private bool hasHit = false;
    public StatusEffectData snareData;
    public event Action OnSnare;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Launch(Vector2 dir, float force)
    {
        direction = dir;
        rb.AddForce(dir * force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit) return;

        if (collision.CompareTag("Player"))
        {
            var statusManager = collision.GetComponent<StatusEffectManager>();
            if (statusManager != null)
            {
                statusManager.AddEffect(new SnareDebuff(snareData));
                OnSnare?.Invoke();
            }
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }
}
