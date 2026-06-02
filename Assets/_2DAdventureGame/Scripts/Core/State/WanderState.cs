using System;
using UnityEngine;

public class WanderState : IState
{
    public MonsterState MonsterState => MonsterState.Wander;

    private AIComponent ai;
    private Vector2 targetPosition;
    private float time = 4f;
    private float timer;
    private bool isWalking;

    public float wanderRadius = 5f;
    public float detectRange = 5f;

    public WanderState(AIComponent ai)
    {
        this.ai = ai;
    }

    public void Enter()
    {
        SetNewDestination();
        isWalking = true;
        timer = time;
    }

    private void SetNewDestination()
    {
        Vector2 currentPosition = ai.transform.position;

        targetPosition = (Vector2)currentPosition +
            UnityEngine.Random.insideUnitCircle * wanderRadius;
    }

    public void Exit()
    {
    }

    public void FixedUpdate()
    {
        float distance = Vector2.Distance(ai.transform.position, targetPosition);
        Vector2 direction = targetPosition- (Vector2)ai.transform.position;

        if (distance < 0.1f)
        {
            if (timer > 0.1f)
            {
                SetNewDestination();
            }
            else
            {
                ai.Movement.StopMonster();
                ai.Animation.SetIdle();
                isWalking = false;
                timer = time;
            }
        }
        else if(isWalking)
        {
            bool flag = ai.Movement.Move(targetPosition);
            ai.Animation.SetMove(direction);
            if (!flag)
            {
                SetNewDestination();
            }
        }

        if (!isWalking && timer <= 0f)
        {
            isWalking = true;
            timer = time;
        }

        timer -= Time.fixedDeltaTime;
    }

    public void Update()
    {

    }
}
