using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PlayerController player;
    public GameObject inventoryUI;
    public GameObject radialMenu;
    public GameObject lootUI;

    private IInteractable currentHover; 
    private IInteractable lootComponent;

    [Header("Player")]
    public InputAction MoveAction; // WASD
    public InputAction LaunchAction; // Mouse Left
    public InputAction RollAction; // Space
    public InputAction InteractionAction; // F
    public InputAction InventoryAction; // I
    public InputAction RadialMenuAction; // E
    public InputAction EscapeAction; // ESC
    public bool IsInventoryOpen => inventoryUI.activeSelf;

    private Vector2 move;

    private void Start()
    {
        MoveAction.Enable();
        LaunchAction.Enable();
        RollAction.Enable();
        InteractionAction.Enable();
        InventoryAction.Enable();
        EscapeAction.Enable();
        RadialMenuAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        // ----- Player Control -------
        move = MoveAction.ReadValue<Vector2>();

        player.SetAnimation(move);

        //Launch
        if (!EventSystem.current.IsPointerOverGameObject() && LaunchAction.WasPressedThisFrame())
        {
            player.Launch();
        }

        // Roll
        if (RollAction.WasPressedThisFrame())
        {
            player.Roll(move);
        }

        // ----- UI -----
        if (EscapeAction.WasPressedThisFrame())
        {
            HandleEscape();
        }

        // Inventory
        if (InventoryAction.WasPressedThisFrame())
        {
            bool isOpen = inventoryUI.activeSelf;
            inventoryUI.SetActive(!isOpen);
        }

        // Radial
        if (RadialMenuAction.IsPressed())
        {
            radialMenu.SetActive(true);
        }

        if (RadialMenuAction.WasReleasedThisFrame())
        {
            radialMenu.SetActive(false);
        }

        // Looting, Interaction
        HandleHover();
        HandleInteractKey();
    }

    private void FixedUpdate()
    {
        player.Move(move);
    }

    void HandleHover()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        IInteractable newHover = null;

        if (hit.collider != null)
        {
            hit.collider.TryGetComponent(out newHover);
        }

        if (currentHover != newHover)
        {
            currentHover?.OnHoverExit();
            newHover?.OnHoverEnter();

            currentHover = newHover;
        }
    }

    void HandleInteractKey()
    {
        if (InteractionAction.WasPressedThisFrame())
        {
            if (lootComponent != null)
            {
                lootComponent.QuitInteract(); // ∑Á∆√ √¢ ¥›±‚
                inventoryUI.SetActive(false);
                lootComponent = null;
                player.EndLooting();
                return;
            }

            else if (currentHover != null && currentHover.IsInteractable())
            {
                lootComponent = currentHover;
                lootComponent.StartInteract();
                player.Loot();
            }
        }
    }

    void HandleEscape()
    {
        if (IsInventoryOpen)
        {
            inventoryUI.SetActive(false);
        }
        if (lootComponent != null)
        {
            player.EndLooting();
            lootComponent.QuitInteract();
            lootComponent = null;
        }
    }
}
