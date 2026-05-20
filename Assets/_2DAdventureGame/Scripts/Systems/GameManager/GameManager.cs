using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public enum GameState
{
    MainMenu,
    GamePlay,
    UI,
    Pause
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState CurrentState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 오브젝트를 파괴하지 않음
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
    }

}
