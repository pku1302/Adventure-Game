using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFlashLight : MonoBehaviour
{
    public Transform lightTransform;
    void Update()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f;

        lightTransform.position = worldPos;
    }
}
