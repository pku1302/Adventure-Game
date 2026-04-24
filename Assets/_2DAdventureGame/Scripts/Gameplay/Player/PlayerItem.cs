using System.Collections;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    public InventoryUI inventory;
    private Coroutine currentCoroutine;
    public bool isUsing { get; private set; }

    public System.Action<float> OnUseProgress;
    public System.Action OnUseStart;
    public System.Action OnUseEnd;

    // Update is called once per frame
    void Awake()
    {
        isUsing = false;
    }

    private void Update()
    {
        if (isUsing && InputManager.Instance.WasMouseRightClicked)
        {
            CancelUse();
        }
    }

    public void UseItem(int index)
    {
        if (isUsing) return;

        var item = inventory.GetItem(index);
        if (item == null) return;
        if (!item.CanUse()) return;

        if (item is ConsumableItem consumableItem)
        {
            currentCoroutine = StartCoroutine(UseCoroutine(consumableItem, index));
        }
    }

    public void CancelUse()
    {
        if (!isUsing) return;

        StopCoroutine(currentCoroutine);
        EndUse();
    }

    private void EndUse()
    {
        isUsing = false;
        currentCoroutine = null;

        OnUseProgress?.Invoke(0f);
        OnUseEnd?.Invoke();
    }

    private IEnumerator UseCoroutine(ConsumableItem item, int index)
    {
        isUsing = true;
        float timer = 0f;
        OnUseStart?.Invoke();

        while (timer < item.useTime)
        {
            timer += Time.deltaTime;

            float progress = timer / item.useTime;
            OnUseProgress?.Invoke(progress);


            yield return null;
        }

        item.Use(gameObject);
        inventory.RemoveItem(index, false);

        EndUse();
    }
}
