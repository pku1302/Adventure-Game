using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public enum GameState
{
    Hub,
    Dungeon,
    Loading,
    Paused
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
        PlayerHealth.OnSpawned += RegisterPlayer;
    }

    void RegisterPlayer(PlayerHealth player)
    {
        player.OnDeath += HandlePlayerDeath;
    }

    private void Start()
    {
    }

    private void HandlePlayerDeath()
    {
        ReturnToHub();
    }

    public void EnterDungeon()
    {
        ChangeState(GameState.Dungeon);
        SceneManager.LoadScene("DungeonScene");
    }

    public void ReturnToHub()
    {
        ChangeState(GameState.Loading);
        SceneManager.LoadScene("HubScene");
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
    }
}
