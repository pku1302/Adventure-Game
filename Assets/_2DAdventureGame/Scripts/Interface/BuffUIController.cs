using System.Collections.Generic;
using UnityEngine;

public class BuffUIController : MonoBehaviour
{
    public Transform container;
    public GameObject iconPrefab;
    public StatusEffectManager character;

    private Dictionary<string, BuffIconUI> icons = new();

    private void Start()
    {
        // character.OnEffectAdded += AddEffectIcon;
        character.OnEffectRemoved += RemoveEffectIcon;
        character.OnEffectUpdated += UpdateEffectIcon;  
        character.OnEffectActivated += AddEffectIcon;
    }

    public void AddEffectIcon(StatusEffect effect)
    {
        var obj = Instantiate(iconPrefab, container);
        var ui = obj.GetComponent<BuffIconUI>();

        ui.Init(effect);
        icons.Add(effect.effectID, ui);
    }

    public void RemoveEffectIcon(StatusEffect effect)
    {
        if (icons.TryGetValue(effect.effectID, out var ui))
        {
            Destroy(ui.gameObject);
            icons.Remove(effect.effectID);
        }
    }

    public void UpdateEffectIcon(StatusEffect effect)
    {
       
    }
}
