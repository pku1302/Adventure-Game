using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LootComponent : MonoBehaviour, IInteractable
{
    public List<ItemSlot> lootItems = new List<ItemSlot>();
    public LootUI lootUI;
    public InventoryUI inventoryUI;
    public bool isLootingDone = false;
    private SpriteRenderer spriteRenderer;
    private float interactDistance = 1.5f;
    private AIComponent ai;
    private bool isHovering = false;

    public void OnHoverExit()
    {
        isHovering = false;
        spriteRenderer.color = Color.white;
    }

    public void OnHoverEnter()
    {
        isHovering = true;
        spriteRenderer.color = Color.gray;
    }

    void HandleHover()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null)
        {
            OnHoverEnter();
        }
        else if (isHovering)
        {
            OnHoverExit();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ai = GetComponent<AIComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.WasInteractionPressed && isHovering)
        {
            float distance = Vector2.Distance(ai.target.position, transform.position);
            if (distance > interactDistance)
            {
                Debug.Log("거리가 멀어");
                return;
            }

            PlayerController playerController = FindFirstObjectByType<PlayerController>();
            playerController.LootStart();
            StartInteract();
        }
        if (InputManager.Instance.WasEscapeActionPressed)
        {
            PlayerController playerController = FindFirstObjectByType<PlayerController>();
            playerController.LootEnd();
            lootUI.Close();
        }
        HandleHover();
    }

    public void StartInteract()
    {
        inventoryUI.TurnOn();
        lootUI.OpenLootUI(this);
    }
  
    public void QuitInteract()
    {
        lootUI.CloseLootUI();
    }
}
