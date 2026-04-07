using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    public GameObject attackEffectPrefab;
    public Vector2 direction;
    GameObject effect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnAttackEffect()
    {
        if (effect) return;

        Vector3 spawnPos = transform.position + (Vector3)(direction * 0.8f);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        effect = Instantiate(attackEffectPrefab, spawnPos, Quaternion.Euler(0,0,angle));
        Destroy(effect, 0.5f);
    }


}
