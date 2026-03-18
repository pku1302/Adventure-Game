using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }


    public GameObject Spawn(GameObject prefab, Vector2 position)
    {
        return Instantiate(prefab, position, Quaternion.identity);
    }

    // Update is called once per frame
}
