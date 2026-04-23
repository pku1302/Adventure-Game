using UnityEngine;

public class JiangshiAttackComponent : AttackComponent
{
    private float scratchCooldown;
    public GameObject scratchHitboxPrefab;
    GameObject scratchHitbox;
    
    void Start()
    {
        Init();
        scratchCooldown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        scratchCooldown -= Time.deltaTime;
    }

    public void ScratchAttack()
    {
        if (scratchHitbox) return;

        Vector3 spawnPos = transform.position + (Vector3)(direction * 0.2f);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0, 0, angle);
        scratchHitbox = Instantiate(scratchHitboxPrefab, spawnPos, rot);
        Destroy(scratchHitbox, 0.1f);
    }

    public override void AttackEnd()
    {
        base.AttackEnd();
    }

    public override void Attack()
    {
        if (scratchCooldown <= 0f)
        {
            isAttacking = true;
            direction = (PlayerController.player.transform.position - ai.transform.position).normalized;
            ai.Animation.SetAttack(direction);
            scratchCooldown = ai.Monster.Data.attackSpeed;
        }
        else
        {
            ai.Movement.StopMonster();
            ai.Animation.SetStop(direction);
        }
    }
}
