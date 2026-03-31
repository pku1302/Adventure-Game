using UnityEngine;

public static class RarityColorUtility
{
    public static Color GetColor(ItemRarity rarity)
    {
        switch (rarity)
        {
            case ItemRarity.Common: return Color.white;
            case ItemRarity.Rare: return HexToColor("#4080FF");
            case ItemRarity.Epic: return HexToColor("#B34DFF");
            case ItemRarity.Legendary: return HexToColor("#FF9933");
            default: return Color.white;
        }
    }

    private static Color HexToColor(string hex)
    {
        Color color;
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }
}
