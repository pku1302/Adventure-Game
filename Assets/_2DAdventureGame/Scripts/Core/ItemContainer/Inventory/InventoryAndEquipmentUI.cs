using UnityEngine;

public class InventoryAndEquipmentUI : MonoBehaviour
{
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private EquipmentUI equipmentUI;

    public void Init(IGamePresenter presenter,
        Inventory inventory,
        EquipmentContainer equipment)
    {
        inventoryUI.Init(presenter, inventory, presenter);
        equipmentUI.Init(presenter, equipment, presenter);
    }
}
