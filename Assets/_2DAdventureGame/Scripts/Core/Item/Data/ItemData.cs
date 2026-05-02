using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public abstract class ItemData : ScriptableObject
{
    [Header("기본 정보")]
    public string itemName;
    public string description;
    public Sprite icon;
    public ItemRarity rarity;
    public ItemType itemType;
    public int maxStack;
    public int sellPrice;
}

public enum ItemType
{
    Equipment,
    Consumable,
    Sellable,
    Key
}

public enum ItemRarity
{
    Common,
    Rare,
    Epic,
    Legendary
}