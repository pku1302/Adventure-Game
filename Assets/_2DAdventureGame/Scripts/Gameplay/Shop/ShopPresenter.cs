using UnityEngine;

public interface IShopPresenter
{
    bool OnRightClickShopSlot(ItemContainer container, int index);
}

public class ShopPresenter : IItemSlotPresenter, IInteractionPresenter, IShopPresenter
{
    private ShopContainer currentShop;
    private ShopService shopService;
    private Inventory inventory;
    private UIManager uiManager;

    public ShopPresenter(ShopService shopService, Inventory inventory, UIManager uiManager)
    {
        this.shopService = shopService;
        this.inventory = inventory;
        this.uiManager = uiManager;
    }

    public void OpenShop(ShopContainer shop)
    {
        currentShop = shop;

        uiManager.OpenShop(
            this,
            shop,
            inventory,
            this
        );
    }

    public InventoryItem GetSlotData(ItemContainer container, int index)
    {
        return container.GetSlotItem(index);
    }

    public bool OnRightClickShopSlot(ItemContainer container, int index)
    {
        if (currentShop == null)
            return false;

        if (container is ShopContainer shop)
        {
            return shopService.Buy(
                currentShop,
                inventory,
                index
            );
        }

        else if (container is Inventory inv)
        {
            return shopService.Sell(inventory, index);
        }

        return false;
    }

    public void Interact(IInteractable target)
    {
        if (target is ShopNPC npc)
        {
            OpenShop(npc.container);
        }
    }
}
