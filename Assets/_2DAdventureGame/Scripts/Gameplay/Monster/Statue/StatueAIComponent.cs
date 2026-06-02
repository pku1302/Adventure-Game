using System;
using UnityEngine;

public class StatueAIComponent : AIComponent
{
    private float stateChangeTimer;
    private float berserkGage = 0;
    private float flashlightGage = 0;
    private AttackState attackState;
    private bool isAttacking;
    private bool isInFlashlight = false;
    private bool isStareMode = false;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip berserkSFX;

    public int maxAngry = 3;
    public bool isBerserk = false;
    public int maxBerserk = 10;
    public Animator animator;

    [SerializeField]
    private StatueAttackComponent Attack;

    private void Awake()
    {
        Monster = GetComponent<Monster>();
        Init();
        attackState = new AttackState(this, Attack);
    }

    void HandleHit(bool isCritical)
    {
        Hit.TakeHit(isCritical);
        stateMachine.ChangeState(hitState);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        stateMachine.Initialize(wanderState);
        Health.OnHit += HandleHit;
        stateChangeTimer = 0.1f;
        Attack.OnAttackEnd += EndAttack;
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        if (!isAttacking && stateChangeTimer <= 0f)
        {
            stateChangeTimer = 0.1f;
            Think();
        }
        stateChangeTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
        if (!isInFlashlight)
        {
            flashlightGage -= Time.fixedDeltaTime;
            flashlightGage = Mathf.Clamp(flashlightGage, 0f, 5f);
        }
    }

    void Think()
    {
        if (TryDead()) return;
        if (TryBerserk()) return;
        //if (TryRun()) return;
        if (TryAttack()) return;
        if (TryChase()) return;
        if (TryStop()) return;

        TryWander();
    }

    bool TryDead()
    {
        if (stateMachine.CurrentState.MonsterState == MonsterState.Dead)
        {
            Health.OnHit -= HandleHit;
            return true;
        }

        return false;
    }

    bool TryBerserk()
    {
        if (!isBerserk && berserkGage >= maxBerserk)
        {
            audioSource.PlayOneShot(berserkSFX);
            isBerserk = true;
            stateMachine.ChangeState(new BerserkState(this));
            return true;
        }
        return false;
    }

    bool TryAttack()
    {
        if (IsPlayerInAttackRange() && isBerserk)
        {
            stateMachine.ChangeState(attackState);
            isAttacking = true;
            stateChangeTimer = Monster.Data.attackSpeed;
            return true;
        }

        return false;
    }

    bool TryRun()
    {
        if (flashlightGage > 0.5f && !isBerserk)
        {
            stateMachine.ChangeState(wanderState);
            isStareMode = false;
            return true;
        }

        return false;
    }

    void EndAttack()
    {
        isAttacking = false;
    }

    bool TryStop()
    {
        if (isBerserk)
        {
            return false;
        }
        if (IsPlayerInDetectRange() || isStareMode)
        {
            isStareMode = true;
            stateMachine.ChangeState(stopState);
            berserkGage += 1f;
            return true;
        }

        return false;
    }

    bool TryChase()
    {
        if (targetHealth.isDead)
        {
            return false;
        }
        if (isBerserk)
        {
            stateMachine.ChangeState(chaseState);
            return true;
        }
        else if (isStareMode && !IsPlayerInStareRange())
        {
            stateMachine.ChangeState(chaseState);
            return true;
        }

        return false;
    }

    bool TryWander()
    {
        stateMachine.ChangeState(wanderState);
        return true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Flashlight"))
            return;
        DetectFlashlight();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInFlashlight = false;
    }

    private void DetectFlashlight()
    {
        isInFlashlight = true;
        flashlightGage += Time.fixedDeltaTime;
        flashlightGage = Mathf.Clamp(flashlightGage, 0f, 5f);
    }

    private bool IsPlayerInStareRange()
    {
        if (target == null)
        {
            return false;
        }

        if (targetHealth.isDead)
        {
            return false;
        }

        float distance = Vector2.Distance(target.position, transform.position);

        return distance <= Monster.Data.detectRange * 2;
    }
}
