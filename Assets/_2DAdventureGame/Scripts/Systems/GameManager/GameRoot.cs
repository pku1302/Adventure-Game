using JetBrains.Annotations;
using UnityEngine;



public class GameRoot : MonoBehaviour
{
    private static GameRoot instance;
    public GameState CurrentState { get; private set; }

    [SerializeField] private UIManager uiManager;
    [SerializeField] private GoldManager goldManager;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private InputManager inputManager;

    private GamePresenter m_presenter;
    private ShopPresenter m_shopPresenter;
    private ProgressController m_progressController;
    private PlayerItem m_playerItem;
    private DeathHandler m_deathHandler;
    private LootService m_lootService;

    public PlayerItem PlayerItem => m_playerItem;
    public GamePresenter GamePresenter => m_presenter;
    public ProgressController ProgressController => m_progressController;
    public ShopPresenter ShopPresenter => m_shopPresenter;
    public DeathHandler DeathHandler => m_deathHandler;
    public LootService LootService => m_lootService;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        var progressController = new ProgressController(uiManager.ProgressUI);
        PlayerItem playerItem = new PlayerItem(progressController);
        var inventory = new Inventory(playerItem);
        var equipment = new EquipmentContainer();
        var sceneService = new SceneService();

        m_progressController = progressController;
        m_playerItem = playerItem;

        inventory.Init(20);
        equipment.Init(4);

        var lootService = new LootService(this);
        m_lootService = lootService;

        var enhanceService = new EnhanceService(inventory, goldManager);
        var itemTransfreService = new ItemTransferService();
        var equipmentService = new EquipmentService();
        var shopService = new ShopService(itemTransfreService, goldManager);
        var presenter = new GamePresenter(
            uiManager,
            itemTransfreService,
            inventory,
            lootService,
            equipmentService,
            equipment,
            playerStats,
            enhanceService,
            sceneService
         );
        var deathHandler = new DeathHandler(inventory, equipment, sceneService);
        m_deathHandler = deathHandler;

        inputManager.Init(presenter);
        m_presenter = presenter;

        var shopPresenter = new ShopPresenter(shopService, inventory, uiManager);
        m_shopPresenter = shopPresenter;
        uiManager.Init(presenter, inventory, equipment);

        var mainMenuController = FindFirstObjectByType<MainMenuController>();
        mainMenuController.Init(sceneService);
    }

}
