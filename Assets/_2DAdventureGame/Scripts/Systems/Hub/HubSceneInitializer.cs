using UnityEngine;

public class HubSceneInitializer : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private HoverDetector hoverDetector;

    [SerializeField]
    private StatusEffectManager statusEffectManager;

    void Start()
    {
        var root = FindFirstObjectByType<GameRoot>();
        var interactionService = new InteractionService();
        var interactionController = new InteractionController(
            playerController,
            hoverDetector,
            interactionService,
            root.ProgressController
        );
        var gameManager = FindFirstObjectByType<GameManager>();
        gameManager.ChangeState(GameState.GamePlay);

        playerController.Init(interactionController, root.PlayerItem, root.ProgressController, gameManager);
        root.PlayerItem.Init(statusEffectManager);
        var shopNPCs = FindObjectsByType<ShopNPC>(FindObjectsSortMode.None);
        var enhanceNPCs = FindObjectsByType<EnhanceNPC>(FindObjectsSortMode.None);
        var loots = FindObjectsByType<LootComponent>(FindObjectsSortMode.None);
        var dungeonNPCs = FindObjectsByType<DungeonNPC>(FindObjectsSortMode.None);

        foreach (var loot in loots)
        {
            loot.Init(root.GamePresenter, true);
        }

        foreach (var npc in shopNPCs)
        {
            npc.Init(root.ShopPresenter);
        }

        foreach (var npc in enhanceNPCs)
        {
            npc.Init(root.GamePresenter);
        }

        foreach (var npc in dungeonNPCs)
        {
            npc.Init(root.GamePresenter);
        }
    }

}
