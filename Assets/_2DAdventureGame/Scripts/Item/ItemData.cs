using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public abstract class ItemData : ScriptableObject
{
    [Header("기본 정보")]
    public string itemName;
    public string description;
    public Sprite icon;
    public ItemRarity rarity;
    public int maxStack;

    public abstract bool CanUse();
    public abstract void Use(GameObject player);
}
