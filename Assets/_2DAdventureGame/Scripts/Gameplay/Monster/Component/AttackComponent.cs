using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public abstract class AttackComponent : MonoBehaviour
{
    public event Action OnAttackEnd;
    public bool isAttacking;

    protected Monster monster;
    protected Vector2 direction;
    protected AIComponent ai;

    protected void Init()
    {
        monster = GetComponent<Monster>();
        ai = GetComponent<AIComponent>();
    }

    // 애니메이터에서 호출
    public virtual void AttackEnd()
    {
        OnAttackEnd?.Invoke();
        isAttacking = false;
    }

    public virtual void Attack()
    {

    }

    public virtual void SetAttackInterrupt()
    {
        isAttacking = false;
        OnAttackEnd?.Invoke();
    }

    public void SetAttackStart(Vector2 dir)
    {
        direction = dir;
    }
}
