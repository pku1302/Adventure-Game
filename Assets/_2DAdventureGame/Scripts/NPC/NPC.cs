using UnityEngine;

public abstract class NPC : MonoBehaviour, IInteractable, IHoverable
{
    [SerializeField]
    protected SpriteRenderer sprite;
    private CursorManager cursorManager;


    protected IInteractionPresenter presenter;
    public virtual float HoldTime { get; set; }

    public Transform GetTransform()
    {
        return transform;
    }

    public virtual void Init(IInteractionPresenter presenter, CursorManager cursorManager)
    {
        this.presenter = presenter;
        this.cursorManager = cursorManager;
    }

    public void Interact()
    {
        presenter.Interact(this);
    }

    public virtual void OnHoverEnter()
    {
        sprite.color = Color.gray;
        cursorManager.SetInteractionCursor();
    }

    public virtual void OnHoverExit()
    {
        sprite.color = Color.white;
        cursorManager.SetDefaultCursor();
    }

    private void OnDestroy()
    {
        cursorManager.SetDefaultCursor();
    }
}
