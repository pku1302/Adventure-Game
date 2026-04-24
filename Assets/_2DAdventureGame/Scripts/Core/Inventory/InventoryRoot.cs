using UnityEngine;

public class InventoryRoot : MonoBehaviour
{
    public InventoryUI inventory;

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.WasInventoryActionPressed)
        {
            inventory.Toggle();
        }

        if (InputManager.Instance.WasEscapeActionPressed)
        {
            inventory.TurnOff();
        }
    }
}
