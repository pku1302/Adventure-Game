using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    [SerializeField] private InventoryAndEquipmentUI inventoryAndEquipmentUI;
    [SerializeField] private LootUI lootUI;
    [SerializeField] private ShopUI shopUI;
    [SerializeField] private EnhanceUI enhanceUI;
    [SerializeField] private DungeonSelectUI dungeonSelectUI;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ProgressUI progressUI;
    [SerializeField] private Image tutorialUI;
    [SerializeField] private UIAudioPlayer uiAudioPlayer;
    [SerializeField] public ConfirmUI confirmUI;

    public ProgressUI ProgressUI { get { return progressUI; } }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void CloseAllUI()
    {
        lootUI.TurnOff();
        CloseInventory();
        shopUI.gameObject.SetActive(false);
        enhanceUI.gameObject.SetActive(false);
        dungeonSelectUI.gameObject.SetActive(false);
        CloseTutorial();
        gameManager.ChangeState(GameState.GamePlay);
    }

    public void Init(GamePresenter presenter, Inventory inventory, EquipmentContainer equipment)
    {
        inventoryAndEquipmentUI.Init(presenter, inventory, equipment);
        dungeonSelectUI.Init(presenter);
    }

    public void OpenDungeonInfo()
    {
        dungeonSelectUI.gameObject.SetActive(true);
        gameManager.ChangeState(GameState.UI);
    }

    public void CloseDungeonInfo()
    {
        dungeonSelectUI.gameObject.SetActive(false);
        gameManager.ChangeState(GameState.GamePlay);
    }

    public void OpenTutorial()
    {
        if (!tutorialUI.gameObject.activeSelf)
        {
            uiAudioPlayer.PlayMap();
            tutorialUI.gameObject.SetActive(true);
            gameManager.ChangeState(GameState.UI);
        }
    }

    public void CloseTutorial()
    {
        tutorialUI.gameObject.SetActive(false);
    }

    public void OpenShop(IItemSlotPresenter presenter, ShopContainer shop, Inventory inventory, IShopPresenter shopPresenter)
    {
        shopUI.Init(presenter, shop, inventory, shopPresenter);
        shopUI.gameObject.SetActive(true);
        gameManager.ChangeState(GameState.UI);

    }

    public void OpenConfirmPopup(string text, bool isCancelActive, Action yesAction, Action noAction = null)
    {
        confirmUI.Open(text, isCancelActive, yesAction, noAction);
        gameManager.ChangeState(GameState.UI);

    }

    public void OpenEnhance(IItemSlotPresenter presenter, Inventory inventory, IEnhancePresenter enhancePresenter)
    {
        enhanceUI.gameObject.SetActive(true);
        enhanceUI.Init(presenter, inventory, enhancePresenter);
        gameManager.ChangeState(GameState.UI);

    }

    public void SetEnhanceItem(InventoryItem item)
    {
        enhanceUI.SetTarget(item);
    }

    public void OpenInventory()
    {
        inventoryAndEquipmentUI.gameObject.SetActive(true);
        gameManager.ChangeState(GameState.UI);

    }

    public void CloseInventory()
    {
        inventoryAndEquipmentUI.gameObject.SetActive(false);
    }

    public void ToggleInventory()
    {
        if (inventoryAndEquipmentUI.gameObject.activeSelf)
        {
            CloseInventory();
        }
        else
        {
            OpenInventory();
        }
    }

    public void OpenLoot(IItemSlotPresenter presenter, ItemContainer container, ILootPresenter lootPresenter)
    {
        lootUI.Init(presenter, container, lootPresenter);
        lootUI.gameObject.SetActive(true);
        OpenInventory();
    }

    public void StartLoot()
    {
        lootUI.ShowLootingLoadingUI();
    }

    public void EndLoot()
    {
        lootUI.HideLootingLoadingUI();
    }

    public void CloseLoot()
    {
        lootUI.gameObject.SetActive(false);
    }

}
