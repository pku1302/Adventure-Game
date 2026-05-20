using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

public class ContextMenuUI : MonoBehaviour
{
    private IContextMenuPresenter presenter;
    public static ContextMenuUI Instance;
    public int currentIndex;
    public GameObject root;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }


    public void Show(IContextMenuPresenter presenter, Vector3 pos, int index)
    {
        root.SetActive(true);
        currentIndex = index;
        this.presenter = presenter;
        root.transform.position = pos + new Vector3(32f, 0f);
    }

    public void Hide()
    {
        root.SetActive(false);
    }

    public void OnClickUse()
    {
        presenter.UseItem(currentIndex);
        Hide();
    }

    public void OnClickDrop()
    {
        presenter.DropItem(currentIndex);
        Hide();
    }
}
