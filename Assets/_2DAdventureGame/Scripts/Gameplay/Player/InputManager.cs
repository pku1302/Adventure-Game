using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject radialMenu;
    public GameObject lootUI;
    public static InputManager Instance;

    [Header("Player")]
    public InputAction MoveAction; // WASD
    public InputAction LaunchAction; // Mouse Left
    public InputAction RollAction; // Space
    public InputAction InteractionAction; // F
    public InputAction InventoryAction; // I
    public InputAction RadialMenuAction; // E
    public InputAction EscapeAction; // ESC
    public InputAction MouseRightAction; // Mouse Right
    public InputAction RunAction; // Shift
    public bool IsInventoryOpen => inventoryUI.activeSelf;
    public bool IsRunning { get; private set; }
    public bool WasMouseRightClicked { get; private set; }
    public bool WasLaunchActionPressed { get; private set; }
    public bool WasRollActionPressed { get; private set; }
    public bool WasInteractionPressed { get; private set; }
    public bool WasInventoryActionPressed { get; private set; }
    public bool WasEscapeActionPressed { get; private set; }
    public Vector2 move { get; private set; }

    // public PlayerItem itemHandler;
    // public PlayerController player;
    // public PlayerDash dash;
    // public FlashLightToggle flashLight;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    private void Start()
    {
        MoveAction.Enable();
        MouseRightAction.Enable();
        LaunchAction.Enable();
        RollAction.Enable();
        InteractionAction.Enable();
        InventoryAction.Enable();
        EscapeAction.Enable();
        RadialMenuAction.Enable();
        RunAction.Enable();

        IsRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        // ----- Player Control -------
        move = MoveAction.ReadValue<Vector2>();

        if (RunAction.IsPressed())
        {
            IsRunning = true;
        }

        if (RunAction.WasReleasedThisFrame())
        {
            IsRunning = false;
        }

        WasMouseRightClicked = MouseRightAction.WasPressedThisFrame();

        WasLaunchActionPressed = !EventSystem.current.IsPointerOverGameObject() && LaunchAction.WasPressedThisFrame();

        WasRollActionPressed = RollAction.WasPressedThisFrame();

        WasInventoryActionPressed = InventoryAction.WasPressedThisFrame();

        WasInteractionPressed = InteractionAction.WasPressedThisFrame();

        WasEscapeActionPressed = EscapeAction.WasPressedThisFrame();
        
        // Radial
        if (RadialMenuAction.IsPressed())
        {
            radialMenu.SetActive(true);
        }

        if (RadialMenuAction.WasReleasedThisFrame())
        {
            radialMenu.SetActive(false);
        }
    }
}
