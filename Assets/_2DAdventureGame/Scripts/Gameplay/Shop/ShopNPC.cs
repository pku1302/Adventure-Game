using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : NPC
{
    [SerializeField]
    private List<ItemData> stockItems;
    public ShopContainer container { get; private set; }

    void Start()
    {
        container = new ShopContainer();
        container.Init(stockItems);
    }

    void Update()
    {
        
    }
}
