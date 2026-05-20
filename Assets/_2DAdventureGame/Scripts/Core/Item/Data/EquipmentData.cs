using UnityEngine;



[CreateAssetMenu(menuName = "Item/Equipment")]
public class EquipmentData : ItemData, IEquipable
{
    public float attack;
    public float defense;
    public string id;
    public EquipmentData nextEnhanceItem;
    public EnhanceCost enhanceCost;
}

[System.Serializable]
public class EnhanceCost
{
    public ItemData material;
    public int materialCount;

    public int gold;
}
