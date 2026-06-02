using UnityEngine;
using UnityEngine.EventSystems;

public class CursorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    protected CursorManager cursorManager;

    [SerializeField]
    private UIAudioPlayer audioPlayer;

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        cursorManager.SetHandCursor();
        audioPlayer.PlayHover();
    }

    public void OnPointerExit(PointerEventData eventData) 
    {
        cursorManager.SetDefaultCursor();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        audioPlayer.PlayClick();
        cursorManager.SetDefaultCursor();
    }

    public void OnClickFunction()
    {
        audioPlayer.PlayClick();
        cursorManager.SetDefaultCursor();
    }


}
