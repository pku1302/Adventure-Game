using UnityEngine;
using NUnit.Framework;
using System.Collections;
using System;

public class LootService
{
    private Coroutine currentLootRoutine;
    private MonoBehaviour runner;

    public LootService(MonoBehaviour runner)
    {
        this.runner = runner;
    }

    public void StartLoot(LootComponent target, Action onComplete)
    {
        if (currentLootRoutine != null)
            runner.StopCoroutine(currentLootRoutine);

        float lootTime = GetLongestTime(target);

        if (!target.isLootingDone)
            currentLootRoutine = runner.StartCoroutine(LootCoroutine(target, lootTime, onComplete));
    }

    public void StopLoot()
    {
        if (currentLootRoutine != null)
            runner.StopCoroutine(currentLootRoutine);
    }

    IEnumerator LootCoroutine(LootComponent target, float lootTime, Action onComplete)
    {
        float timer = 0f;

        while (timer < lootTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        onComplete?.Invoke();
        target.isLootingDone = true;
    }

    public float GetLootTime(ItemRarity rarity)
    {
        switch (rarity)
        {
            case ItemRarity.Common: return 0.1f;
            case ItemRarity.Rare: return 2f;
            case ItemRarity.Epic: return 5f;
            case ItemRarity.Legendary: return 8f;
            default: return 0.1f;
        }
    }

    public float GetLongestTime(LootComponent target)
    {
        float maxTime = 0f;
        ItemContainer container = target.container;

        for (int i = 0; i < container.GetSlotCount(); i++)
        {
            var item = container.GetSlotItem(i);

            if (item == null) continue;

            float lootTime = GetLootTime(item.data.rarity);
            
            if (maxTime < lootTime)
                maxTime = lootTime;
        }

        return maxTime;
    }
}
