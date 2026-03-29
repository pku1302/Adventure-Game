using UnityEngine;

public class LootUI : MonoBehaviour
{
    public GameObject panel;
    public Transform slotParent;
    public GameObject slotPrefab;

    public LootComponent currentLoot;
    public Inventory playerInventory;

    public void Toggle(LootComponent root)
    {
        currentLoot = root;
        bool isOpen = panel.activeSelf;
        panel.SetActive(!isOpen);

        Refresh();
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
