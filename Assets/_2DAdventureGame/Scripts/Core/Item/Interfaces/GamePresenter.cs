using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IInteractionPresenter
{
    void Interact(IInteractable target);
}

public interface IGamePresenter :
    IInteractionPresenter,
    IInventoryPresenter,
    IContextMenuPresenter,
    IItemSlotPresenter,
    IEquipmentPresenter,
    ILootPresenter,
    IEnhancePresenter,
    IDungeonPresenter
{
}

public interface ILootPresenter
{
    void TakeItem(ItemContainer container, int index);
    void OnDrop(ItemContainer from, ItemContainer to, int fromIndex, int toIndex, int amount, bool isSplitMode);
}

public interface IEnhancePresenter
{
    void EnhanceItem(InventoryItem item);
    void SelectEnhanceItem(InventoryItem item);
}

public interface IInventoryPresenter
{
    void ToggleInventory();
    void OpenContextMenu(int index, Vector3 position);
    void OnDrop(ItemContainer from, ItemContainer to, int fromIndex, int toIndex, int amount, bool isSplitMode);
    void EquipItem(int index);
}

public interface IEquipmentPresenter
{
    void UnequipItem(int index);
}

public interface IContextMenuPresenter
{
    void UseItem(int index);
    void DropItem(int index);
}

public interface IItemSlotPresenter
{
    InventoryItem GetSlotData(ItemContainer container, int index);
}

public interface IDungeonPresenter
{
    void TryEnterDungeon(DungeonData data);
}

public class GamePresenter : IGamePresenter
{
    private EquipmentService equipmentService;
    private EnhanceService enhanceService;
    private EquipmentContainer equipment;
    private PlayerStats playerStat;
    private UIManager uiManager;
    private ItemTransferService itemTransferService;
    private Inventory inventory;
    private LootService lootService;
    private SceneService sceneService;

    public event Action OnLootComplete;

    public GamePresenter(UIManager uiManager,
        ItemTransferService iTs, 
        Inventory inv, 
        LootService ls, 
        EquipmentService es, 
        EquipmentContainer e, 
        PlayerStats ps, 
        EnhanceService ehs, 
        SceneService sc)
    {
        this.uiManager = uiManager;
        itemTransferService = iTs;
        inventory = inv;
        lootService = ls;
        equipmentService = es;
        equipment = e;
        OnLootComplete += HandleComplete;
        playerStat = ps;
        enhanceService = ehs;
        sceneService = sc;
    }

    public void EquipItem(int inventoryIndex)
    {
        equipmentService.Equip(inventory, equipment, playerStat, inventoryIndex);
    }

    public void ToggleInventory()
    {
        uiManager.ToggleInventory();
    }

    public void OpenContextMenu(int index, Vector3 position)
    {
        ContextMenuUI.Instance.Hide();
        ContextMenuUI.Instance.Show(this, position, index);
    }

    public void UseItem(int index)
    {
        var item = inventory.GetSlotItem(index);

        if (item == null) return;

        inventory.UseItem(item, index);
    }

    public void SelectEnhanceItem(InventoryItem item)
    {
        if (item == null) return;

        if (item.data is not EquipmentData e)
            return;

        uiManager.SetEnhanceItem(item);
    }

    public void DropItem(int index)
    {
        var item = inventory.GetSlotItem(index);

        if (item == null) return;

        inventory.RemoveItem(item);
    }

    public void TryEnterDungeon(DungeonData data)
    {
        uiManager.OpenConfirmPopup(
            $"[{data.dungeonName}]에 입장하시겠습니까?",
            () =>
            {
                EnterDungeon();
                uiManager.CloseDungeonInfo();
            });
    }

    private void EnterDungeon()
    {
        sceneService.LoadDungeon();
    }

    public void OnDrop(
    ItemContainer from,
    ItemContainer to,
    int fromIndex,
    int toIndex,
    int amount,
    bool isSplitMode
    )
    {
        if (from == null || to == null) return;

        itemTransferService.Move(from, to, amount, fromIndex, toIndex, isSplitMode);
    }

    public InventoryItem GetSlotData(ItemContainer container, int index)
    {
        return container.GetSlotItem(index);
    }


    public void Interact(IInteractable target)
    {
        if (target is LootComponent loot)
        {
            uiManager.OpenLoot(this, loot.container, this);
            
            if (!loot.isLootingDone)
            {
                lootService.StartLoot(loot, OnLootComplete);
                uiManager.StartLoot();
            }
        }
        else if (target is EnhanceNPC enhanceNPC)
        {
            uiManager.OpenEnhance(this, inventory, this);
        }
        else if (target is DungeonNPC dungeonNPC)
        {
            uiManager.OpenDungeonInfo();
        }
        else if (target is RecallNPC recallNPC)
        {
            sceneService.LoadHub();
        }
    }

    public void EnhanceItem(InventoryItem item)
    {
        enhanceService.Enhance(item);
    }

    private void HandleComplete()
    {
        uiManager.EndLoot();
    }

    public void TakeItem(ItemContainer container, int index)
    {
        var item = container.GetSlotItem(index);
        itemTransferService.Move(container, inventory, item.count, index);
    }

    public void UnequipItem(int index)
    {
        equipmentService.Unequip(inventory, equipment, playerStat, index);
    }
}
