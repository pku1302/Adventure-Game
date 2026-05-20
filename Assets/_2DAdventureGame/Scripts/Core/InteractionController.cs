using Unity.VisualScripting;
using UnityEngine;

public class InteractionController
{
    private PlayerController player;
    private HoverDetector hoverDetector;
    private InteractionService interactionService;
    private ProgressController progressController;
    private IInteractable currentTarget;
    private float holdTimer = 0f;
    private bool isHolding;

    public InteractionController(
        PlayerController player,
        HoverDetector hoverDetector,
        InteractionService interactionService,
        ProgressController progressController
        )
    {
        this.player = player;
        this.hoverDetector = hoverDetector;
        this.interactionService = interactionService;
        this.progressController = progressController;
    }

    public IInteractable BeginInteract()
    {
        IInteractable target = hoverDetector.CurrentTarget;

        if (target == null)
        {
            return null;
        }

        if (!interactionService.CanInteract(player, target))
        {
            return null;
        }

        if (target.HoldTime <= 0)
        {
            target.Interact();
            return target;
        }

        if (isHolding)
        {
            return null;
        }

        currentTarget = target;

        holdTimer = 0f;

        isHolding = true;

        progressController.Begin(
         currentTarget.HoldTime,
         () =>
         {
             currentTarget.Interact();
             CancleInteract();
         });

        return target;
    }

    public void Tick()
    {
        if (!isHolding)
        {
            return;
        }

        if (currentTarget == null)
        {
            CancleInteract();
            return;
        }

        progressController.Tick();

    }

    public void CancleInteract()
    {
        isHolding = false;

        holdTimer = 0f;

        currentTarget = null;

        progressController.Cancel();
    }
}
