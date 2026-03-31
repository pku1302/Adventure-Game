using NUnit.Framework;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;


public class LootUI : MonoBehaviour
{
    public GameObject panel;
    public Transform slotParent;
    public GameObject slotPrefab;

    public LootComponent currentLoot;
    public Inventory playerInventory;

    [SerializeField] private LootOverlayController lootOverlay;
    private Coroutine currentLootRoutine;


    public void OpenLootUI(LootComponent root)
    {
        currentLoot = root;
        bool isOpen = panel.activeSelf;
        panel.SetActive(!isOpen);
        StartLoot();

        Refresh();
    }

    public void CloseLootUI()
    {
        if (currentLootRoutine != null)
            StopCoroutine(currentLootRoutine);

        lootOverlay.Hide();
        panel.SetActive(false);
    }

    public void StartLoot()
    {
        if (currentLootRoutine != null)
            StopCoroutine(currentLootRoutine);

        if (!currentLoot.isLootingDone)
            currentLootRoutine = StartCoroutine(LootItem());
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

    public float GetLongestTime()
    {
        float maxTime = 0f;

        foreach (var item in currentLoot.lootItems)
        {
            float lootTime = GetLootTime(item.item.rarity);
            if (maxTime < lootTime)
                maxTime = lootTime;
        }

        return maxTime;
    }

    IEnumerator LootItem()
    {
        float lootTime = GetLongestTime();
        float timer = 0f;

        lootOverlay.Show();

        while (timer < lootTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        lootOverlay.Hide();
        currentLoot.isLootingDone = true;
    }

    public void Refresh()
    {
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < currentLoot.lootItems.Count; i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            slot.GetComponent<LootSlotUI>().SetSlot(currentLoot.lootItems[i], this, playerInventory, i);
        }
    }

    public void Close()
    {
        panel.SetActive(false);
    }
}
