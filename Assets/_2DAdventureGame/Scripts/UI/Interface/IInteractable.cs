using UnityEngine;
using System;

public interface IInteractable
{
    float HoldTime { get; set; }
    void OnHoverExit();
    void OnHoverEnter();
    Transform GetTransform();
    void Interact();
}


