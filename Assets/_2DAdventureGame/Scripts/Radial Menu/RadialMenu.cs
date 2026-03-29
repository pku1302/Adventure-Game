using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject radialMenu;
    public RectTransform[] slots;

    [Header("Settings")]
    public int slotCount = 5;
    public float deadZone = 1f;
    public float highlightScale = 1.3f;

    private int currentIndex = -1;

    public void SetCurrentIndex(int index)
    {
        currentIndex = index;
        Image slot = slots[currentIndex].Find("Image").GetComponent<Image>();

        if (slot != null)
        {
            slot.color = new Color(0.6f, 0.6f, 0.6f);
        }
        
    }

    public void ClearIndex(int index)
    {
        Image slot = slots[currentIndex].Find("Image").GetComponent<Image>();

        if (slot != null)
        {
            slot.color = new Color(1f, 1f, 1f);
        }

        if (currentIndex == index)
            currentIndex = -1;
    }

    void OnDisable()
    {
        if (currentIndex != -1)
        {
            SelectSlot(currentIndex);
        }

        Reset();
    }

    private void Reset()
    {
        for (int i = 0; i < slotCount; i++)
        {
            Image slot = slots[i].Find("Image").GetComponent<Image>();
            slot.color = new Color(1f, 1f, 1f);
        }

        currentIndex = -1;

    }

    void SelectSlot(int index)
    {
        Debug.Log("º±≈√µ» ΩΩ∑‘: " + index);
    }
}
