using UnityEngine;

public class FindFoodState 
{
    //private AttackMonsterAIComponent ai;
    //private Food targetFood = null;
    //private float timer;
    //private float timeInterval = 1f;

    //public FindFoodState(AttackMonsterAIComponent ai)
    //{
    //    this.ai = ai;
    //}

    //public void Enter()
    //{
    //    targetFood = FindNearestFood();
    //    timer = timeInterval;
    //}

    //public void Exit()
    //{
    //}

    //public void FixedUpdate()
    //{
    //    if (targetFood != null)
    //    {
    //        ai.Movement.Move(targetFood.transform.position, true);
    //    }
    //    else
    //    {
    //        ai.Movement.Move(ai.transform.position, false);
    //    }
    //}

    //public void Update()
    //{
    //    if (timer <= 0f)
    //    {
    //        if (targetFood == null)
    //        {
    //            targetFood = FindNearestFood();
    //        }
    //        else 
    //        {
    //            float distance = Vector2.Distance(targetFood.transform.position, ai.transform.position);

    //            if (distance < 1.0f)
    //            {
    //               ai.Hunger.Eat(targetFood); 
    //            }
    //        }
    //        timer = timeInterval;
    //    }
    //    timer -= Time.deltaTime;
        
    //    if (!ai.Hunger.IsHungry)
    //    {
    //        ai.ChangeState(new WanderState(ai));
    //    }
    //}

    //private Food FindNearestFood()
    //{
    //    Food[] foods = Object.FindObjectsByType<Food>(FindObjectsSortMode.None);

    //    float minDistance = float.MaxValue;
    //    Food nearest = null;

    //    foreach (var food in foods)
    //    {
    //        float distance = Vector2.Distance(ai.transform.position, food.transform.position);

    //        if (distance < minDistance)
    //        {
    //            minDistance = distance;
    //            nearest = food;
    //        }
    //    }

    //    return nearest;
    //}
}
