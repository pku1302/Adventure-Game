using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public int gold {  get; private set; }

    public System.Action<int> onGoldChanged;

    public void AddGold(int amount)
    {
        if (amount <= 0) return;

        gold += amount;
        onGoldChanged?.Invoke(gold);
    }

    public bool SpendGold(int amount)
    {
        if (amount <= 0) return false;
        if (gold < amount) return false;

        gold -= amount;
        onGoldChanged?.Invoke(gold);

        return true;
    }
}
