using UnityEngine;

public class InventoryRoot : MonoBehaviour
{
    public InventoryUI inventory;
    public EquipmentUI equipment;

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.WasInventoryActionPressed)
        {
            inventory.Toggle();
            equipment.Toggle();
        }

        if (InputManager.Instance.WasEscapeActionPressed)
        {
            equipment.TurnOff();
            inventory.TurnOff();
        }
    }
}
