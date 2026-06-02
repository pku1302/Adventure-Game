using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ShopData", menuName = "Scriptable Objects/ShopData")]
public class ShopData : ScriptableObject
{
    public List<ShopItem> shopItems;
}

[System.Serializable]
public class ShopItem
{
    public ItemData item;
}
