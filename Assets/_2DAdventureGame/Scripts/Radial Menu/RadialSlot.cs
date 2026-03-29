using UnityEngine;
using UnityEngine.EventSystems;

public class RadialSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int index;
    public RadialMenu menu;

    public void OnPointerEnter(PointerEventData eventData)
    {
        menu.SetCurrentIndex(index);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        menu.ClearIndex(index);
    }

}
