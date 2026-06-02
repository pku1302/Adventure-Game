using UnityEngine;

public class HubSceneInitializer : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private HoverDetector hoverDetector;

    [SerializeField]
    private StatusEffectManager statusEffectManager;

    [SerializeField]
    private HPBar hpBar;

    [SerializeField]
    private PlayerHealth playerHealth;

    [SerializeField]
    private PlayerWeapon playerWeapon;

    [SerializeField]
    private PlayerStamina playerStamina;

    [SerializeField]
    private ProgressUI reloadProgressUI;

    void Awake()
    {
        var root = FindFirstObjectByType<GameRoot>();
        var interactionService = new InteractionService();
        var interactionController = new InteractionController(
            playerController,
            hoverDetector,
            interactionService,
            root.ProgressController
        );
        var reloadProgressController = new ProgressController(reloadProgressUI);
        var gameManager = FindFirstObjectByType<GameManager>();
        gameManager.ChangeState(GameState.GamePlay);

        playerController.Init(interactionController, root.PlayerItem, root.ProgressController, gameManager, root.PlayerStats, reloadProgressController);
        root.PlayerItem.Init(statusEffectManager);
        hpBar.Init(root.PlayerStats);
        playerHealth.Init(root.PlayerStats);
        playerWeapon.Init(reloadProgressController, root.PlayerStats);
        root.PlayerStats.Init(playerStamina);

        var shopNPCs = FindObjectsByType<ShopNPC>(FindObjectsSortMode.None);
        var enhanceNPCs = FindObjectsByType<EnhanceNPC>(FindObjectsSortMode.None);
        var loots = FindObjectsByType<LootComponent>(FindObjectsSortMode.None);
        var dungeonNPCs = FindObjectsByType<DungeonNPC>(FindObjectsSortMode.None);
        var tutorialNPCs = FindObjectsByType<TutorialNPC>(FindObjectsSortMode.None);

        foreach (var loot in loots)
        {
            loot.Init(root.GamePresenter, true, root.CursorManager);
        }

        foreach (var npc in shopNPCs)
        {
            npc.Init(root.ShopPresenter, root.CursorManager);
        }

        foreach (var npc in enhanceNPCs)
        {
            npc.Init(root.GamePresenter, root.CursorManager);
        }

        foreach (var npc in dungeonNPCs)
        {
            npc.Init(root.GamePresenter, root.CursorManager);
        }

        foreach (var npc in tutorialNPCs)
        {
            npc.Init(root.GamePresenter, root.CursorManager);
        }
    }

}
