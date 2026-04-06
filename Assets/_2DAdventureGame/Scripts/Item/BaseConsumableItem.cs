using System.Collections;
using UnityEngine;

public abstract class BaseConsumableItem : MonoBehaviour
{
    public float useTime = 1f;

    public void Use()
    {
        StartCoroutine(UseCoroutine());
    }

    private IEnumerator UseCoroutine()
    {
        yield return new WaitForSeconds(useTime);
        ApplyEffect();
    }

    protected abstract void ApplyEffect();
}
