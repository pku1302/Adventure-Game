using UnityEngine;

public class AnimationEventReceiver : MonoBehaviour
{
    [SerializeField]
    private StatueAttackComponent attackComponent;

    public void AttackEnd()
    {
        attackComponent.AttackEnd();
    }

    public void RamAttack()
    {
        attackComponent.RamAttack();
    }
}
