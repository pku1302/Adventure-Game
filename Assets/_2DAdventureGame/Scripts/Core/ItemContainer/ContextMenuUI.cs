using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

public class ContextMenuUI : MonoBehaviour
{
    private InventoryItem item;
    public InventoryUI inventoryUI;
    public static ContextMenuUI Instance;
    public InventoryManager inventory;
    public int index;
    public GameObject root;

    void Awake()
    {
        Instance = this;
        Hide();
    }

    public void Show(InventoryItem item, Vector3 pos, int idx)
    {
        root.SetActive(true);
        this.item = item;
        index = idx;
        root.transform.position = pos + new Vector3(32f, 0f);
    }

    public void Hide()
    {
        root.SetActive(false);
    }

    public void OnClickUse()
    {
        inventory.UseItem(item, index);
        inventoryUI.UpdateUI();
        Hide();
    }

    public void OnClickDrop()
    {
        inventory.RemoveItem(index);
        inventoryUI.UpdateUI();
        Hide();
    }
}
