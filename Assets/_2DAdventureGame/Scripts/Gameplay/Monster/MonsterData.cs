using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Scriptable Objects/MonsterData")]
public class MonsterData : ScriptableObject
{
    public int maxHp;
    public float moveSpeed;
    public float wanderRadius;
    public float attackRange;
    public float attackSpeed;
    public float detectRange;
    public float attackDamage;
}
