using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class AmmoBarUI : MonoBehaviour
{
    [SerializeField]
    private Image slotPrefab;

    [SerializeField]
    private Transform slotParent;

    private List<Image> slots = new();

    public void Init(int maxAmmo)
    {
        for (int i = 0; i < maxAmmo; i++)
        {
            Image slot = Instantiate(slotPrefab, slotParent);

            if (i == maxAmmo - 1)
            {
                slot.color = Color.red;
            }
            slots.Add(slot);
        }
    }

    public void ConsumeAmmo(int currentAmmo)
    {
        if (currentAmmo < 0 || currentAmmo > slots.Count)
        {
            return;
        }
        int maxAmmo = slots.Count;

        Image slot = slots[maxAmmo - currentAmmo - 1];
        Color color = slot.color;
        color.a = 0f;
        slots[maxAmmo - currentAmmo - 1].color = color;
    }

    public void Refill()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Image slot = slots[i];
            Color color = slot.color;
            color.a = 1f;
            slots[i].color = color;
        }
    }
}
