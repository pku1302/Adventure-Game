using System;
using UnityEngine;

public class StatueAttackComponent : AttackComponent
{
    [SerializeField]
    private Collider2D hitbox;
    [SerializeField]
    private HealthComponent health;
    private float ramAttackCooldown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
        hitbox.enabled = false;
        ramAttackCooldown = 0f;
        health.OnDeath += OffTrigger; 
    }

    // Update is called once per frame
    void Update()
    {
        ramAttackCooldown -= Time.deltaTime;
    }

    private void OffTrigger()
    {
        hitbox.enabled = false;
    }

    public void RamAttack()
    {
        hitbox.enabled = true;
    }

    public override void AttackEnd()
    {
        base.AttackEnd();
        hitbox.enabled = false;
        ramAttackCooldown = ai.Monster.Data.attackSpeed;
    }

    public override void Attack()
    {
        if (ramAttackCooldown <= 0f && !isAttacking)
        {
            isAttacking = true;
            direction = (ai.target.position - ai.transform.position).normalized;
            ai.Animation.SetAttack(direction);
            ai.Movement.Rush(direction * 4f);
        }
        else if (isAttacking)
        {
            ai.Movement.Rush(direction * 4f);
        }
        else
        {
            ai.Movement.StopMonster();
            ai.Animation.SetStop(direction);
        }
    }

    public override void SetAttackInterrupt()
    {
        base.SetAttackInterrupt();
        ramAttackCooldown = ai.Monster.Data.attackSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(monster.Data.attackDamage, DamageType.Normal);
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            hitbox.enabled = false;

            if (rb != null)
            {
                Vector2 dir = (collision.gameObject.transform.position - ai.transform.position).normalized;
                player.ApplyKnockback(dir, 10f, 0.2f);
            }
        }
    }
}
