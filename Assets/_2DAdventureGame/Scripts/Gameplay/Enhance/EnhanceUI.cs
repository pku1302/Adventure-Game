using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class EnhanceUI : MonoBehaviour
{
    [SerializeField]
    private InventoryForEnhanceUI inventoryForEnhanceUI;

    [SerializeField]
    private DisplayItemSlotUI materials;

    [SerializeField]
    private DisplayItemSlotUI targetItemSlot;

    [SerializeField]
    private TMP_Text goldText;

    [SerializeField]
    private DisplayItemSlotUI resultItemSlot;

    [SerializeField]
    private CursorManager cursorManager;

    private IEnhancePresenter enhancePresenter;
    private IItemSlotPresenter presenter;
    private InventoryItem currentItem;
    private Inventory inventory;

    public void Init(IItemSlotPresenter presenter, Inventory inventory, IEnhancePresenter enhancePresenter)
    {
        this.enhancePresenter = enhancePresenter;
        inventoryForEnhanceUI.Init(presenter, inventory, enhancePresenter);
        Clear();
        Refresh();
        this.presenter = presenter;
        inventory.OnChanged += RefreshInventory;
    }

    private void RefreshInventory()
    {
        inventoryForEnhanceUI.RefreshUI();
    }

    public void SetTarget(InventoryItem item)
    {
        currentItem = item;

        Refresh();
    }

    private void Refresh()
    {
        if (currentItem == null)
        {
            Clear();
            return;
        }

        targetItemSlot.Set(currentItem);

        var equipmentData = currentItem.data as EquipmentData;
        var nextItem = equipmentData.nextEnhanceItem;

        if (nextItem != null)
        {
            resultItemSlot.Set(new InventoryItem(nextItem, 1));
        }
        else
        {
            resultItemSlot.Clear();
        }

        inventoryForEnhanceUI.RefreshUI();
        RefreshMaterials();

        goldText.text = equipmentData.enhanceCost.gold.ToString() + "$";
    }

    private void RefreshMaterials()
    {
        var equipmentData = currentItem.data as EquipmentData;
        var cost = equipmentData.enhanceCost;

        if (cost.material == null)
        {
            materials.Set(null);
            return;
        }

        var item = new InventoryItem(cost.material, cost.materialCount);

        materials.Set(item);
    }

    public void OnClickEnhance()
    {
        if (currentItem == null)
        {
            return;
        }

        if (enhancePresenter.EnhanceItem(currentItem))
        {
            cursorManager.OnClickEnhance();
        }
        else
        {
            cursorManager.OnClickFail();
        }
    }

    private void Clear()
    {
        targetItemSlot.Clear();
        resultItemSlot.Clear();
        materials.Clear();

        goldText.text = "";
        currentItem = null;
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
