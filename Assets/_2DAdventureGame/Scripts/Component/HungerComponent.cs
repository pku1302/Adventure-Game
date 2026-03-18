using System;
using UnityEngine;


public class HungerComponent : MonoBehaviour
{
    private StatusComponent statusComponent;
    private AttackMonsterAIComponent ai;
    private bool hungryFlag = false;
    private float timer;
    private float timeInterval = 2f;

    public float hunger = 0f;
    public float maxHunger = 100f;
    public float hungerIncreaseSpeed = 10f;

    public bool IsHungry => hunger >= 70f;
    public bool IsStarving => hunger >= 95f;
    public event Action OnHungry;
    public event Action OnStarving;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        statusComponent = GetComponent<StatusComponent>();
        ai = GetComponent<AttackMonsterAIComponent>();
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0f)
        {
            UpdateHunger();
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer = 0f;
            }
        }

        if (IsHungry && !hungryFlag)
        {
            OnHungry?.Invoke();
            hungryFlag = true;
        }
        if (IsStarving)
        {
            statusComponent.AddStatus(new StarvingEffect(ai));
        }
    }

    private void UpdateHunger()
    {
        hunger += hungerIncreaseSpeed * Time.deltaTime;

        if(hunger > maxHunger)
            hunger = maxHunger;
    }

    public void Eat(Food targetFood)
    {
        int gainSatiety = targetFood.Consume();
        hungryFlag = false;
        timer = timeInterval;
        hunger -= gainSatiety;
    }
}
