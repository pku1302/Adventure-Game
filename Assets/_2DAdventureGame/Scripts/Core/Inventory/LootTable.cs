using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LootTable", menuName = "Scriptable Objects/LootTable")]
public class LootTable : ScriptableObject
{
    [System.Serializable]
    public class LootEntry
    {
        public ItemData item;
        [Range(0f, 1f)]
        public float dropChance;
        public int minAmount;
        public int maxAmount;
    }

    public List<LootEntry> lootEntries = new List<LootEntry>();

    public List<ItemSlot> GenerateLoot()
    {
        List<ItemSlot> result = new List<ItemSlot>();

        foreach (var entry in lootEntries)
        {
            float roll = Random.value;

            if (roll <= entry.dropChance)
            {
                int amount = Random.Range(entry.minAmount , entry.maxAmount + 1);

                result.Add(new ItemSlot(entry.item, amount));
            }
        }

        return result;
    }

}
