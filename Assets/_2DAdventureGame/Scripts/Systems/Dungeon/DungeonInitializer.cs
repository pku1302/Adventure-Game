using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class DungeonInitializer : MonoBehaviour
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

    public Action OnDungeonGenerated;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

        var gameManager = FindFirstObjectByType<GameManager>();
        var reloadProgressController = new ProgressController(reloadProgressUI);

        gameManager.ChangeState(GameState.GamePlay);
        root.PlayerItem.Init(statusEffectManager);
        hpBar.Init(root.PlayerStats);
        playerWeapon.Init(reloadProgressController, root.PlayerStats);
        playerHealth.Init(root.PlayerStats);
        playerController.Init(interactionController, root.PlayerItem, root.ProgressController, gameManager, root.PlayerStats, reloadProgressController);
        root.PlayerStats.Init(playerStamina);

        playerHealth.OnDeath += root.DeathHandler.HandleDeath;
        StartCoroutine(GenerateDungeon());
    }

    IEnumerator GenerateDungeon()
    {
        Monster[] monsters = FindObjectsByType<Monster>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (Monster monster in monsters)
        {
            monster.gameObject.SetActive(true);
            yield return null;
        }

        var root = FindFirstObjectByType<GameRoot>();
        var loots = FindObjectsByType<LootComponent>(FindObjectsSortMode.None);
        var recallNPCs = FindObjectsByType<RecallNPC>(FindObjectsSortMode.None);

        foreach (var loot in loots)
        {
            loot.Init(root.GamePresenter, true, root.CursorManager);
        }

        foreach (var npc in recallNPCs)
        {
            npc.Init(root.GamePresenter, root.CursorManager);
        }

        yield return null;

        OnDungeonGenerated?.Invoke();
    }

}
