using System.Collections;
using System.Threading;
using UnityEngine;

public class WorkMonsterAIComponent : AIComponent
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float timer;
    private float timeInterval = 15f;

    public GameObject foodPrefab;

    private void Awake()
    {
        Init();
    }
    void Start()
    {
        stateMachine.Initialize(new WanderState(this));
        timer = timeInterval;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Debug.Log("음식 생성");
            stateMachine.ChangeState(new CreateFoodState(this));
            timer = timeInterval;
        }
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        if (Health.IsDead)
            return;
        stateMachine.FixedUpdate();
    }


}
