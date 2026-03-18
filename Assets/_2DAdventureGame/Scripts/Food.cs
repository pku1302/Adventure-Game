using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private FoodData data;
    public int nutrition;
    public int currentEatCount;
    public FoodData Data => data;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nutrition = data.nutrition;
        currentEatCount = data.maxEatCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int Consume()
    {
        currentEatCount--;

        if(currentEatCount <= 0)
        {
            Destroy(gameObject);
        }

        return nutrition;
    }
}
