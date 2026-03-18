using UnityEngine;

public class AttackMonsterAIComponent : AIComponent
{
    public float detectRange = 5f;
    public AttackComponent Attack;

    void Awake()
    {
        Init();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateMachine.Initialize(new WanderState(this));
        Attack = GetComponent<AttackComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Health.IsDead)
            return;
        stateMachine.Update();
    }

    private void FixedUpdate()
    {

        if (Health.IsDead)
            return;
        stateMachine.FixedUpdate();
    }

}
