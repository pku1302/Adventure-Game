using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Scriptable Objects/MonsterData")]
public class MonsterData : ScriptableObject
{
    public float moveSpeed;
    public float wanderRadius;
    public float attackRange;
    public int maxHp;
    public float attackSpeed;
}
