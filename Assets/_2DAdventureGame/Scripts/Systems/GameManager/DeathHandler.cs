using UnityEngine;

public class DeathHandler
{
    private Inventory inventory;

    private EquipmentContainer equipment;

    private SceneService sceneService;

    public DeathHandler(Inventory inventory, EquipmentContainer equipment, SceneService sceneService)
    {
        this.inventory = inventory;
        this.equipment = equipment;
        this.sceneService = sceneService;
    }

    public void HandleDeath()
    {
        inventory.Clear();

        equipment.Clear();

        sceneService.LoadHub();
    }
}
