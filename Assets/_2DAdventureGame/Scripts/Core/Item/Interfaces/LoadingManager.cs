using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] 
    private DungeonInitializer dungeonInitializer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        dungeonInitializer.OnDungeonGenerated += CloseLoadingUI;
    }

    private void CloseLoadingUI()
    {
        gameObject.SetActive(false);
    }
}
