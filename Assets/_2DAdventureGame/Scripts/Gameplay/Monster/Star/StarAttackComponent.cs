using UnityEngine;

public class StarAttackComponent : AttackComponent
{
    private float slashCooldown;
    public GameObject attackEffectPrefab;
    GameObject effect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Init();
        slashCooldown = 0f;
    }

    private void Update()
    {
        slashCooldown -= Time.deltaTime;
    }

    public void Slash()
    {
        if (effect) return;

        Vector3 spawnPos = transform.position + (Vector3)(direction * 0.8f);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        effect = Instantiate(attackEffectPrefab, spawnPos, Quaternion.Euler(0, 0, angle));
        Destroy(effect, 0.5f);
    }

    public override void AttackEnd()
    {
        base.AttackEnd();
    }

    public override void Attack()
    {
        if (slashCooldown <= 0f)
        {
            isAttacking = true;
            direction = (PlayerController.player.transform.position - ai.transform.position).normalized;
            ai.Animation.SetAttack(direction);
            slashCooldown = ai.Monster.Data.attackSpeed;
        }
        else
        {
            ai.Movement.StopMonster();
            ai.Animation.SetStop(direction);
        }
    }

}
