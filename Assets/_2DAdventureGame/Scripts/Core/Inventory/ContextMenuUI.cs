using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

public class ContextMenuUI : MonoBehaviour
{
    public static ContextMenuUI Instance;
    public PlayerItem player;
    public Inventory inventory;
    public int index;
    public GameObject root;

    void Awake()
    {
        Instance = this;
        Hide();
    }

    public void Show(InventorySlot item, Vector3 pos, int idx)
    {
        index = idx;
        root.SetActive(true);
        root.transform.position = pos + new Vector3(132f, 0f);
    }

    public void Hide()
    {
        root.SetActive(false);
    }

    public void OnClickUse()
    {
        player.UseItem(index);
        inventory.gameObject.SetActive(false);
        Hide();
    }

    public void OnClickDrop()
    {
        inventory.RemoveItem(index, true);
        Hide();
    }
}
