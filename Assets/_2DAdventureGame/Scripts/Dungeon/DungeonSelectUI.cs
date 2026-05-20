using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;


public class DungeonSelectUI: MonoBehaviour
{
    [SerializeField]
    private List<DungeonSlotUI> slots;

    [SerializeField]
    private DungeonInfoUI infoUI;
    private IDungeonPresenter presenter;

    public void Init(IDungeonPresenter presenter)
    {
        this.presenter = presenter;
        foreach(var slot in slots)
        {
            slot.Init(presenter);   
        }
    }

    private void OnEnable()
    {
        infoUI.Hide();
    }

    public void ShowDungeonInfo(DungeonData data)
    {
        infoUI.Show(data);
    }

    public void HideDungeonInfo()
    {
        infoUI.Hide();
    }

}