using UnityEngine;

public interface IInteractable
{
    void OnHoverExit();
    void OnHoverEnter();
    void StartInteract();
    void QuitInteract();
}
