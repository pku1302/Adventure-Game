using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class DebuffRadialUIController : MonoBehaviour
{
    public GameObject radialUIPrefab;
    public StatusEffectManager player;
    private List<DebuffUI> debuffUIs = new List<DebuffUI>();

    private void Start()
    {
        player.OnEffectAdded += CreateUI;
        player.OnEffectUpdated += UpdateUIs;
    }

    private void CreateUI(StatusEffect effect)
    {
        if (!effect.data.isDebuff) return;

        GameObject go = Instantiate(radialUIPrefab, transform);
        DebuffUI ui = go.GetComponent<DebuffUI>();
        ui.Init(effect);
        debuffUIs.Add(ui);
    }

    public void UpdateUIs(StatusEffect effect)
    {
        if (!effect.data.isDebuff) return;

        for (int i = 0; i < debuffUIs.Count; i++)
        {
            debuffUIs[i].SetUI(i);
        }
    }
}
