using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneService
{
    public void LoadHub()
    {
        SceneManager.LoadScene("HubScene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void LoadDungeon()
    {
        SceneManager.LoadScene("DungeonScene");
    }
}
