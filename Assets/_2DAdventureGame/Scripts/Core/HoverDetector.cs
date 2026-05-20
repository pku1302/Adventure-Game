using UnityEngine;
using UnityEngine.InputSystem;

public interface IHoverable
{
    void OnHoverEnter();
    void OnHoverExit();
}

public class HoverDetector : MonoBehaviour
{
    public IInteractable CurrentTarget { get; private set; }
    int layerMask;

    private void Start()
    {
        layerMask = ~LayerMask.GetMask("Flashlight", "Monster Detection");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Collider2D hit = Physics2D.OverlapPoint(worldPos, layerMask);

        if (hit != null)
        {
            var target = hit.GetComponent<IInteractable>();
            CurrentTarget?.OnHoverExit();
            CurrentTarget = target;
            CurrentTarget?.OnHoverEnter();
        }
        else
        {
            CurrentTarget?.OnHoverExit();
            CurrentTarget = null;
        }
    }
}
