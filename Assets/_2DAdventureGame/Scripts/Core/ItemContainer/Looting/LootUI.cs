using NUnit.Framework;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using System.Collections.Generic;

public class LootUI : ItemContainerUI
{
    public LootComponent currentLoot; // 몬스터 아이템이 담겨 있는 곳
    public GameObject panel;
    public InventoryManager playerInventory;
    public InventoryUI inventoryUI;

    [SerializeField] private LootOverlayController lootOverlay; // 가림막
    private Coroutine currentLootRoutine;

    public override void UpdateUI()
    {
        int slotCount = currentLoot.GetSlotCount();

        for (int i = 0; i < slotCount; i++)
        {
            slotUIs[i].SetSlot(this, currentLoot.GetSlotItem(i), i);
        }
    }

    public void OpenLootUI(LootComponent root)
    {
        currentLoot = root;
        bool isOpen = panel.activeSelf;
        panel.SetActive(!isOpen);
        Refresh();
        StartLoot();
    }

    protected override void CreateSlotUIs()
    {
        for (int i = 0; i < currentLoot.GetSlotCount(); i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            LootSlotUI ui = slot.GetComponent<LootSlotUI>();
            slotUIs.Add(ui);
        }
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

        for (int i = 0; i < currentLoot.GetSlotCount(); i++)
        {
            float lootTime = GetLootTime(currentLoot.GetSlotItem(i).data.rarity);
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
        CreateSlotUIs();
        UpdateUI();
    }

    public void Close()
    {
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }
        foreach (var slot in slotUIs)
        {
            Destroy(slot.gameObject);
        }
        slotUIs.Clear();    
        panel.SetActive(false);
    }


}
