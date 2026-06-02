using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public Bullet bulletPrefab;
    public float reloadTime;
    public int maxAmmo;
}
