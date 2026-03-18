using System.Collections;
using UnityEngine;

public class HitComponent : MonoBehaviour
{
    public static HitComponent instance;
    private AIComponent ai;
    private AudioSource audioSource;
    public AudioClip[] hitSFXs;
    public GameObject hitEffectPrefab;
    public GameObject hitEffectPrefab2;
    public GameObject hitEffectPrefab3;
    public GameObject hitEffectPrefab4;
    public int effectCount = 32;
    public float radius = 0.5f;
    public float duration = 0.3f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ai = GetComponent<AIComponent>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeHit(int damage, Vector2 direction)
    {
        ai.Health.TakeDamage(damage);
        ai.Animation.SetHit(direction);
        AudioClip clip = hitSFXs[Random.Range(0, hitSFXs.Length)];
        audioSource.PlayOneShot(clip);
        SpawnHitEffect();
        GameObject effect1 = Instantiate(hitEffectPrefab2, ai.transform.position, Quaternion.identity);
        GameObject effect2 = Instantiate(hitEffectPrefab3, ai.transform.position, Quaternion.identity);
        GameObject effect3 = Instantiate(hitEffectPrefab4, ai.transform.position, Quaternion.identity);
        effect1.AddComponent<FadeOut>();
        effect2.AddComponent<FadeOut>();
        Destroy(effect1, 0.3f);
        Destroy(effect2, 0.05f);
    }

    public void SpawnHitEffect()
    {
        Vector3 center = transform.position;
        

        for (int i = 0; i< effectCount; i++)
        {
            float randomAngle = Random.Range(0f, Mathf.PI * 2);
            Vector2 dir = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));

            Vector3 targetPos = center + (Vector3)(dir * radius);
            GameObject effect = Instantiate(hitEffectPrefab, targetPos, Quaternion.identity);
            effect.transform.localScale = new Vector3(2f, 1f, 1f);

            SpriteRenderer spriteRenderer = effect.GetComponent<SpriteRenderer>();
            effect.AddComponent<FadeOut>();

            Vector3 lookDir = (center - targetPos).normalized;
            float rotZ = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            rotZ += 90f;
            effect.transform.rotation = Quaternion.Euler(0, 0, rotZ);

            Destroy(effect, duration);
        }
    }
}
