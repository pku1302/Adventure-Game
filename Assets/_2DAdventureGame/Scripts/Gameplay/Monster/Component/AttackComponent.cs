using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public abstract class AttackComponent : MonoBehaviour
{
    public event Action OnAttackEnd;
    public bool isAttacking;

    protected Vector2 direction;
    [SerializeField]
    protected AIComponent ai;
    [SerializeField]
    protected Monster monster; 

    protected void Init()
    {
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

    public virtual void SetAttackStart(Vector2 dir)
    {
        direction = dir;
    }
}
