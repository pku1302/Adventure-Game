using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private ShopContainerUI shopContainerUI;
    [SerializeField] private InventoryShopUI inventoryUI;
    public void Init(IItemSlotPresenter presenter, ShopContainer shop, Inventory inventory, IShopPresenter shopPresenter)
    {
        shopContainerUI.Init(presenter, shop, shopPresenter);
        inventoryUI.Init(presenter, inventory, shopPresenter);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
