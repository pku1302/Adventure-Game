using UnityEngine;

public class LootOverlayController : MonoBehaviour
{
    [SerializeField] private GameObject lootOverlay;

    public void Show ()
    {
        lootOverlay.SetActive (true);
    }

    public void Hide()
    {
        lootOverlay.SetActive (false);
    }
}
