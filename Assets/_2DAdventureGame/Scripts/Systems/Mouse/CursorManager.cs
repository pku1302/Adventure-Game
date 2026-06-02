using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField]
    private Texture2D handCursor;
    [SerializeField]
    private Texture2D interactCursor;
    [SerializeField]
    private UIAudioPlayer audioPlayer;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetHandCursor()
    {
        Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.Auto);
    }
    public void SetDefaultCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
    public void SetInteractionCursor()
    {
        Cursor.SetCursor(interactCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnClickBuy()
    {
        audioPlayer.PlayBuy();
    }

    public void OnClickFail()
    {
        audioPlayer.PlayCancel();
    }

    public void OnClickEnhance()
    {
        audioPlayer.PlayEnhance();
    }
}
