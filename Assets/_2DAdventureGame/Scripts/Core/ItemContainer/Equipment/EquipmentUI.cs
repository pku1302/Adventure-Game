using UnityEngine;

public class EquipmentUI : ItemContainerUI
{
    public EquipmentManager equipment;
    public InventoryUI inventoryUI;
    public InventoryManager inventory;

    public EquipmentSlotUI helmetSlot;
    public EquipmentSlotUI armorSlot;
    public EquipmentSlotUI pantsSlot;
    public EquipmentSlotUI bootsSlot;
    public EquipmentSlotUI weaponSlot;

    //public enum EquipmentType
    //{
    //    Weapon,
    //    Helmet,
    //    Armor,
    //    Pants,
    //    Boots
    //}

    void Start()
    {
        equipment.OnEquipmentChanged += UpdateUI;
        UpdateUI();
    }

    public override void UpdateUI()
    {
        helmetSlot.SetSlot(this, equipment.GetEquipment(EquipmentType.Helmet), 0);
        armorSlot.SetSlot(this, equipment.GetEquipment(EquipmentType.Armor), 1);
        pantsSlot.SetSlot(this, equipment.GetEquipment(EquipmentType.Pants), 2);
        bootsSlot.SetSlot(this, equipment.GetEquipment(EquipmentType.Boots), 3);
        weaponSlot.SetSlot(this, equipment.GetEquipment(EquipmentType.Weapon), 4);
    }

    protected override void CreateSlotUIs()
    {
        
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
    }

    public void TurnOn()
    {
        gameObject.SetActive(true);
    }
}
