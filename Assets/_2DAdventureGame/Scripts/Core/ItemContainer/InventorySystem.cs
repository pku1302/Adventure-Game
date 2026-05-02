using UnityEngine;

public static class InventorySystem
{
    public static bool Move(
        ContainerManager from,
        ContainerManager to,
        int fromIndex,
        int toIndex,
        int amount
        )
    {
        var item = from.GetSlotItem(fromIndex);
        if (item == null)
        {
            return false;
        }
        if (!to.CanAdd(item, amount, toIndex))
        {
            return false;
        }
        int leftAmount = to.AddItem(item.data, amount, toIndex);
        int movedAmount = amount - leftAmount;

        if (movedAmount > 0)
        {
            from.DecreaseItem(fromIndex, movedAmount);
        }

        return true;
    }

    public static bool Swap(
        ContainerManager from,
        ContainerManager to,
        int fromIndex,
        int toIndex
        )
    {
        var fromItem = from.GetSlotItem(fromIndex);
        var toItem = to.GetSlotItem(toIndex);

        if (fromItem == null && toItem == null)
            return false;

        from.RemoveItem(fromIndex);
        to.RemoveItem(toIndex);

        if (toItem != null)
            from.AddItem(toItem.data, toItem.count, fromIndex);
        if (fromItem != null)
            to.AddItem(fromItem.data, fromItem.count, toIndex);

        return true;
    }
}
