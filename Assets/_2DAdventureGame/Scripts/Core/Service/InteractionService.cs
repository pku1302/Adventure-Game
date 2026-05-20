using UnityEngine;

public class InteractionService
{
    public bool CanInteract(PlayerController player, IInteractable target)
    {
        float distance = Vector3.Distance(player.transform.position, target.GetTransform().position);
     
        if (distance < 2f)
            return true;

        return false;
    }
}
