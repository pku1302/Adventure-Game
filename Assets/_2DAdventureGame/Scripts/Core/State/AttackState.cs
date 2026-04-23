using UnityEngine;

public class AttackState : IState
{
    public MonsterState MonsterState => MonsterState.Attack;
    private AIComponent ai;
    private AttackComponent attackComponent;

    public AttackState(AIComponent ai, AttackComponent attackComponent)
    {
        this.ai = ai;
        this.attackComponent = attackComponent;
    }

    public void Enter()
    {
        Vector2 dir = (PlayerController.player.transform.position - ai.transform.position).normalized;
        attackComponent.SetAttackStart(dir);
    }

    public void Exit()
    {
        attackComponent.AttackEnd();
    }

    public void FixedUpdate()
    {
    }

    public void Update()
    {
        attackComponent.Attack();
    }
}
