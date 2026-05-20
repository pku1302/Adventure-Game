using UnityEngine;

public class PersistentCanvasRoot : MonoBehaviour
{
    private static PersistentCanvasRoot instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }
}
