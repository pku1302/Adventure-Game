using UnityEngine;

public class GhostAttackComponent : AttackComponent
{
    private float thrustCooldown;
    private float snareCooldown;

    public GameObject thrustHitBoxPrefab;
    GameObject thrustHitBox;

    public GameObject snarePrefab;
    GhostSnare snare;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
        snareCooldown = 0f;
        thrustCooldown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        snareCooldown -= Time.deltaTime;
        thrustCooldown -= Time.deltaTime;
    }

    public void ThrustAttack()
    {
        if (thrustHitBox) return;

        Vector3 spawnPos = transform.position + (Vector3)(direction * 0.2f);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0, 0, angle);
        thrustHitBox = Instantiate(thrustHitBoxPrefab, spawnPos, rot);
        Destroy(thrustHitBox, 0.1f);
    }

    public void SnareAttack()
    {
        Vector3 spawnPos = transform.position;
        GameObject snareObject = Instantiate(snarePrefab, spawnPos, Quaternion.identity);
        snare = snareObject.GetComponent<GhostSnare>();
        snare.OnSnare += ThrustCooldownReset;
        snare.Launch(direction, 500);
    }

    public override void AttackEnd()
    {
        base.AttackEnd();
    }

    public void ThrustCooldownReset()
    {
        thrustCooldown = 0f;
        snare.OnSnare -= ThrustCooldownReset;
    }

    public void ThrustAttackDone()
    {
        thrustCooldown = 4f;
        isAttacking = false;
    }

    public override void Attack()
    {
        float distance = Vector2.Distance(transform.position, PlayerController.player.transform.position);
        Vector2 playerPos = (Vector2)PlayerController.player.transform.position;
        direction = (PlayerController.player.transform.position - ai.transform.position).normalized;

        if (snareCooldown <= 0f)
        {
            ai.Movement.StopMonster();
            ai.Animation.SetStop(direction);
            SnareAttack();
            snareCooldown = 10f;
        }
        else if (isAttacking)
        {
            ai.Movement.StopMonster();
        }
        else if (distance > 1f && thrustCooldown <= 0f)
        {
            ai.Movement.Move(playerPos, ai.Monster.Data.moveSpeed * 1.5f);
            ai.Animation.SetMove(direction);
        }
        else if (distance <= 1f && thrustCooldown <= 0f)
        {
            ai.Animation.SetAttack(direction);
            ai.Movement.StopMonster();
            isAttacking = true;
        }
        else
        {
            ai.Movement.Rush(direction * (-1) * ai.Monster.Data.moveSpeed);
            ai.Animation.SetMove(direction * (-1));
        }
    }
}
