using UnityEngine;

public interface IStatusEffect
{
    void Enter();
    void Update();
    void Exit();

    bool IsFinished { get; }
}
