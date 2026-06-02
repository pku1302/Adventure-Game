using UnityEngine;

public class ShopService
{
    private ItemTransferService transferService;
    private GoldManager playerGold;

    public ShopService(ItemTransferService transferService, GoldManager gold)
    {
        this.transferService = transferService;
        playerGold = gold;
    }

    public bool Buy(
        ShopContainer shop,
        Inventory inventory,
        int index)
    {
        var item = shop.GetSlotItem(index);

        if (item == null)
        {
            return false;
        }

        if (inventory.IsFull())
        {
            return false;
        }

        int price = GetBuyPrice(shop, item);

        if (playerGold.gold < price)
        {
            return false;
        }

        playerGold.SpendGold(price);

        transferService.Move(
            shop,
            inventory,
            item.count,
            index
         );

        return true;
    }

    public int GetBuyPrice(
        ShopContainer shop,
        InventoryItem item
        )
    {
        return Mathf.RoundToInt(
            item.data.buyPrice
        );
    }

    public bool Sell(
    Inventory inventory,
    int index)
    {
        var item = inventory.GetSlotItem(index);

        if (item == null)
        {
            return false;
        }

        int price = item.data.sellPrice * item.count;

        playerGold.AddGold(price);

        inventory.RemoveItem(item);

        return true;
    }


}
