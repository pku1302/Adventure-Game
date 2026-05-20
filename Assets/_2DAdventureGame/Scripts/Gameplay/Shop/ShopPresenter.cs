using UnityEngine;

public interface IShopPresenter
{
    void OnRightClickShopSlot(ItemContainer container, int index);
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

    public void OnRightClickShopSlot(ItemContainer container, int index)
    {
        if (currentShop == null)
            return;

        if (container is ShopContainer shop)
        {
            shopService.Buy(
                currentShop,
                inventory,
                index
            );
        }

        else if (container is Inventory inv)
        {
            shopService.Sell(inventory, index);
        }
    }

    public void Interact(IInteractable target)
    {
        if (target is ShopNPC npc)
        {
            OpenShop(npc.container);
        }
    }
}
