using UnityEngine;

public interface IDragSource
{
    public int GetSlotIndex();
    public ItemStorage GetStorageRef();
}
