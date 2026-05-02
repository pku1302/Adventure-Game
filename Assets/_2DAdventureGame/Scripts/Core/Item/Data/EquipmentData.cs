using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Helmet,
    Armor,
    Pants,
    Boots
}

[CreateAssetMenu(menuName = "Item/Equipment")]
public class EquipmentData : ItemData, IEquipable
{
    public EquipmentType equipmentType;
    public float attack;
    public float defense;


}
