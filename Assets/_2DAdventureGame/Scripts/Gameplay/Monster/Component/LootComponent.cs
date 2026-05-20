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
    private IInteractionPresenter presenter;
    public float HoldTime => 0f;

    public ItemContainer container {  get; private set; }
    public bool isAlive {  get; private set; }
    public bool isLootingDone = false;

    private void Start()
    {
        container = new ItemContainer();
        container.Init(lootTable.GenerateLoot());
    }

    public void Interact()
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
        spriteRenderer.color = Color.white;
    }

    public void OnHoverEnter()
    {
        spriteRenderer.color = Color.gray;
    }

    public void Init(IInteractionPresenter presenter, bool isAlive)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.isAlive = isAlive;
        this.presenter = presenter;
    }

    public void InitializeLootItems(List<InventoryItem> items)
    {
        foreach (var item in items)
        {
            container.AddItem(item.data, item.count);
        }
    }
}
