using UnityEngine;

public class Chest : LootComponent
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip openSFX;
    private bool isOpened = false;
    public float posX = 0f;

    public override float HoldTime
    {
        get
        {
            return isOpened ? 0f : 4f;
        }
    }

    public override void Init(IInteractionPresenter presenter, bool isAlive, CursorManager cursorManager)
    {
        base.Init(presenter, false, cursorManager);
        animator.SetFloat("posX", posX);
    }

    public override void Interact()
    {
        animator.SetTrigger("Open");
        presenter.Interact(this);
        if (!isOpened)
        {
            isOpened = true;
            audioSource.PlayOneShot(openSFX);
        }

    }
}
