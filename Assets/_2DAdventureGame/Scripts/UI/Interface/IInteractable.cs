using UnityEngine;

public interface IInteractable
{
    float HoldTime { get;}
    void OnHoverExit();
    void OnHoverEnter();
    Transform GetTransform();
    void Interact();
}


