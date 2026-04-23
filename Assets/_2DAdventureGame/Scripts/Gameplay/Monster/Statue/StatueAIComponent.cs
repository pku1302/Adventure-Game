using System;
using UnityEngine;

public class StatueAIComponent : AIComponent
{
    private float stateChangeTimer;
    private int berserkGage = 0;
    private float flashlightGage = 0;
    private AttackState attackState;
    private bool isAttacking;
    private bool isInFlashlight = false;

    public int maxAngry = 3;
    public bool isBerserk = false;
    public int maxBerserk = 5;
    public Animator animator;

    public StatueAttackComponent Attack { get; private set; }

    private void Awake()
    {
        Monster = GetComponent<Monster>();
        Attack = GetComponent<StatueAttackComponent>();
        Init();
        attackState = new AttackState(this, Attack);
    }

    void HandleHit()
    {
        stateMachine.ChangeState(hitState);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        if (TryAttack()) return;
        if (TryRun()) return;
        if (TryStop()) return;
        if (TryChase()) return;

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
            isBerserk = true;
            stateMachine.ChangeState(new BerserkState(this));
            return true;
        }
        return false;
    }

    bool TryAttack()
    {
        if (IsPlayerInAttackRange())
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
        if (flashlightGage > 1.5f && !isBerserk)
        {
            stateMachine.ChangeState(wanderState);
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
        float playerDistance = Vector2.Distance(transform.position, PlayerController.player.transform.position);
        if (isBerserk || playerDistance > Monster.Data.detectRange)
        {
            return false;
        }
        else 
        {
            stateMachine.ChangeState(stopState);
            berserkGage += 1;
            return true;
        }
    }

    bool TryChase()
    {
        if (isBerserk)
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
}
