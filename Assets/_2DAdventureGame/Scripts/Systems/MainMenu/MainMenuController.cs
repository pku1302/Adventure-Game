using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private SceneService sceneService;

    public void Init(SceneService sceneService)
    {
        this.sceneService = sceneService;
    }

    private void Start()
    {
        var root = FindFirstObjectByType<GameManager>();

        root.ChangeState(GameState.MainMenu);
    }

    public void StartGame()
    {
        sceneService.LoadHub();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
