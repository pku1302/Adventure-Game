using UnityEngine;
using UnityEngine.EventSystems;

public class CursorInteractChanger : CursorChanger
{
    public override void OnPointerEnter(PointerEventData eventData)
    {
        cursorManager.SetInteractionCursor();
    }
}
