using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LootComponent : MonoBehaviour, IInteractable, IHoverable
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private LootTable lootTable;
    [SerializeField]
    private HealthComponent healthComponent;
    private CursorManager cursorManager;
    protected IInteractionPresenter presenter;
    public virtual float HoldTime { get; set; } 

    public ItemContainer container {  get; private set; }
    public bool isAlive {  get; private set; }
    public bool isLootingDone = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        container = new ItemContainer();
        container.Init(lootTable.GenerateLoot());
        if (healthComponent != null)
        {
            healthComponent.OnDeath += SetIsAlive;
        }
    }

    private void SetIsAlive()
    {
        isAlive = false;
    }

    public virtual void Interact()
    {
        if (healthComponent.IsDead)
        {
            presenter.Interact(this);
        }
    }

    public Transform GetTransform()
        { return transform; }

    public void OnHoverExit()
    {
        if (!isAlive)
        {
            cursorManager.SetDefaultCursor();
        }
        spriteRenderer.color = Color.white;
    }

    public void OnHoverEnter()
    {
        if (!isAlive)
        {
            cursorManager.SetInteractionCursor();
        }
        spriteRenderer.color = Color.gray;
    }

    public virtual void Init(IInteractionPresenter presenter, bool isAlive, CursorManager cursorManager)
    {
        this.isAlive = isAlive;
        this.presenter = presenter;
        this.cursorManager = cursorManager;
    }

    public void InitializeLootItems(List<InventoryItem> items)
    {
        foreach (var item in items)
        {
            container.AddItem(item.data, item.count);
        }
    }
}
