using System.Collections;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    public InventoryManager inventory;
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

    public void UseItem(ItemData item)
    {
        if (isUsing) return;

        if (item == null) return;

        if (item is ConsumableData consumableItem)
        {
            currentCoroutine = StartCoroutine(UseCoroutine(consumableItem));
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

    private IEnumerator UseCoroutine(ConsumableData item)
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

        item.Use(gameObject); // 효과 적용
        EndUse();
    }
}
