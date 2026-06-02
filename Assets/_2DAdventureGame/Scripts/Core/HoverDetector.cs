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
        layerMask = LayerMask.GetMask("Monster", "NPC", "Dead Body");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Collider2D[] hits = Physics2D.OverlapPointAll(worldPos, layerMask);
        bool wasHit = false;
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out IInteractable target))
            {
                if (target == CurrentTarget)
                    return;

                CurrentTarget?.OnHoverExit();
                CurrentTarget = target;
                CurrentTarget?.OnHoverEnter();
                wasHit = true;
                break;
            }
        }

        if (CurrentTarget != null && !wasHit)
        {
            CurrentTarget?.OnHoverExit();
            CurrentTarget = null;
        }
    }
}
