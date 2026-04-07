using UnityEngine;

public interface IState
{
    MonsterState MonsterState { get; }
    void Enter();
    void Update();
    void Exit();
    void FixedUpdate();
}
