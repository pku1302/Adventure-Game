using UnityEngine;

public class ItemTransferService
{
    public bool Move(
        ItemContainer from,
        ItemContainer to,
        int amount,
        int fromIndex,
        int toIndex = -1,
        bool isSplitMode = false
        )
    {
        var slot = from.GetSlotItem(fromIndex);
        if (slot == null) return false;

        if (from == to && to.CanSwap(slot.data, toIndex, isSplitMode))
        {
            Swap(to, fromIndex, toIndex);
            return true;
        }

        if (!to.CanAdd(slot.data, slot.count, toIndex))
            return false;

        int leftAmount = to.AddItem(slot.data, amount, toIndex);
        int movedAmount = amount - leftAmount;

        if (movedAmount > 0)
        {
            from.DecreaseItem(slot, movedAmount);
        }

        return true;
    }

    public bool Swap(
        ItemContainer container,
        int indexA,
        int indexB
        )
    {
      return  container.Swap(indexA, indexB);
    }
}
