using System.Collections;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public float duration = 0.3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color color = sr.color;

        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(color.a, 0f, t / duration);

            sr.color = new Color(color.r, color.g, color.b, alpha);

            yield return null;
        }

        Destroy(gameObject);
    }
}
