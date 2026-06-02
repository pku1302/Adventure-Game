using UnityEngine;

public class DeathHandler
{
    private Inventory inventory;

    private EquipmentContainer equipment;

    private SceneService sceneService;

    private ConfirmUI confirmUI; 

    private UIManager uiManager;

    public DeathHandler(Inventory inventory, EquipmentContainer equipment, SceneService sceneService, ConfirmUI confirmUI, UIManager uIManager)
    {
        this.inventory = inventory;
        this.equipment = equipment;
        this.sceneService = sceneService;
        this.confirmUI = confirmUI;
        this.uiManager = uIManager;
    }

    public void HandleDeath()
    {
        uiManager.CloseAllUI();
        confirmUI.Open("모든 아이템을 잃었습니다.\n기지로 복귀합니다.",false,  ReturntoHub);
    }

    private void ReturntoHub()
    {
        inventory.Clear();

        equipment.Clear();

        sceneService.LoadHub();
    }
}
