using UnityEngine;

public class LootTarget
{
    public Transform transform;
    public bool isAlive = false;
    public ItemContainer container;

    public LootTarget(Transform transform, bool isAlive, ItemContainer container)
    {
        this.transform = transform;
        this.isAlive = isAlive;
        this.container = container;
    }
}
