using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private UIManager uiManaer;
    [SerializeField]
    private GameManager gameManager;

    private IInventoryPresenter presenter;

    public static InputManager Instance;

    public InputAction InventoryAction; // I
    public InputAction RadialMenuAction; // E
    public InputAction EscapeAction; // ESC
    public InputAction MouseRightAction; // Mouse Right

    public bool WasMouseRightClicked { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        MouseRightAction.Enable();
        InventoryAction.Enable();
        EscapeAction.Enable();
        RadialMenuAction.Enable();
    }

    public void Init(IInventoryPresenter presenter)
    {
        this.presenter = presenter;
    }

    // Update is called once per frame
    void Update()
    {
        WasMouseRightClicked = MouseRightAction.WasPressedThisFrame();

        if (InventoryAction.WasPressedThisFrame())
        {
            presenter.ToggleInventory();
        }

        if (gameManager.CurrentState == GameState.UI
            && EscapeAction.WasPressedThisFrame())
        {
            uiManaer.CloseAllUI();
        }

        // Radial
        //if (RadialMenuAction.IsPressed())
        //{
        //    radialMenu.SetActive(true);
        //}

        //if (RadialMenuAction.WasReleasedThisFrame())
        //{
        //    radialMenu.SetActive(false);
        //}
    }
}
