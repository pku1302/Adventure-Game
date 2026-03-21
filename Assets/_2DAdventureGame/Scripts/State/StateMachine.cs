using UnityEngine;

public class StateMachine
{
    private IState currentState;
    public IState CurrentState => currentState;

    public void Initialize(IState startState)
    {
        currentState = startState;
        currentState.Enter();
    }

    public void ChangeState(IState newState)
    {
        if (currentState.MonsterState == newState.MonsterState) return;

        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        currentState?.Update();
    }

    public void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }
}
