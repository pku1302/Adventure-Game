using UnityEngine;

public abstract class NPC : MonoBehaviour, IInteractable, IHoverable
{
    [SerializeField]
    protected SpriteRenderer sprite;
    protected IInteractionPresenter presenter;
    public virtual float HoldTime => 0f;

    public Transform GetTransform()
    {
        return transform;
    }

    public virtual void Init(IInteractionPresenter presenter)
    {
        this.presenter = presenter;
    }

    public void Interact()
    {
        presenter.Interact(this);
    }

    public virtual void OnHoverEnter()
    {
        sprite.color = Color.gray;
    }

    public virtual void OnHoverExit()
    {
        sprite.color = Color.white;
    }
}
