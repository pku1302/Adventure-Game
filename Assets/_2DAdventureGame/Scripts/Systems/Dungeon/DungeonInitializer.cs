using UnityEngine;
using System;

public class DungeonInitializer : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private HoverDetector hoverDetector;

    [SerializeField]
    private StatusEffectManager statusEffectManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        root.PlayerItem.Init(statusEffectManager);
        playerController.Init(interactionController, root.PlayerItem, root.ProgressController, gameManager);
        var loots = FindObjectsByType<LootComponent>(FindObjectsSortMode.None);
        var recallNPCs = FindObjectsByType<RecallNPC>(FindObjectsSortMode.None);
        var playerHealth = FindFirstObjectByType<PlayerHealth>();
        playerHealth.OnDeath += root.DeathHandler.HandleDeath;

        foreach (var loot in loots)
        {
            loot.Init(root.GamePresenter, true);
        }

        foreach (var npc in recallNPCs)
        {
            npc.Init(root.GamePresenter);
        }
    }

}
