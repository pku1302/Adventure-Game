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

    private IEnhancePresenter enhancePresenter;
    private InventoryItem currentItem;

    public void Init(IItemSlotPresenter presenter, Inventory inventory, IEnhancePresenter enhancePresenter)
    {
        this.enhancePresenter = enhancePresenter;
        inventoryForEnhanceUI.Init(presenter, inventory, enhancePresenter);
        Clear();
        Refresh();
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

        RefreshMaterials();

        goldText.text = equipmentData.enhanceCost.gold.ToString();
    }

    private void RefreshMaterials()
    {
        var equipmentData = currentItem.data as EquipmentData;
        var cost = equipmentData.enhanceCost;

        if (cost.material == null)
            return;

        var item = new InventoryItem(cost.material, cost.materialCount);

        materials.Set(item);
    }

    public void OnClickEnhance()
    {
        if (currentItem == null)
        {
            return;
        }

        enhancePresenter.EnhanceItem(currentItem);

        Refresh();
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
