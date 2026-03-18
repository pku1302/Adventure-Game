using UnityEngine;

public abstract class AIComponent : MonoBehaviour
{
    public MovementComponent Movement { get; private set; }
    public AnimationComponent Animation { get; private set; }
    public HealthComponent Health { get; private set; }
    
    public HitComponent Hit { get; private set; }

    protected StateMachine stateMachine;

    protected void Init()
    {
        Movement = GetComponent<MovementComponent>();
        Animation = GetComponent<AnimationComponent>();
        Health = GetComponent<HealthComponent>();
        Hit = GetComponent<HitComponent>();

        stateMachine = new StateMachine();

        if (Health != null)
        {
            Health.OnDeath += HandleDeath;
        }
    }

    public void ChangeState(IState newState)
    {
        stateMachine.ChangeState(newState);
    }

    private void HandleDeath()
    {

    }

}
