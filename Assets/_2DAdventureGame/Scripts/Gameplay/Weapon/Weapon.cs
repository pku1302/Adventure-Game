using System;
using UnityEditor;
using UnityEngine;

public class Weapon
{
    private WeaponData data;
    public int CurrentAmmo { get; private set; }
    public int MaxAmmo { get; private set; }

    public event Action<int> OnAmmoChanged;
    public event Action OnReloadStart;
    public event Action OnReloadEnd;

    private Bullet bulletPrefab;
    public float ReloadTime { get; private set; }
    public bool IsReloading { get; private set; }
    private ProgressController progressController;
    public bool IsLastShot { get; private set; }

    public Weapon(WeaponData data, ProgressController progressController)
    {
        this.data = data;
        this.progressController = progressController;

        ReloadTime = data.reloadTime;
        MaxAmmo = data.maxAmmo;
        bulletPrefab = data.bulletPrefab;
        CurrentAmmo = MaxAmmo;
    }

    public bool Fire(Vector2 direction, Vector2 firePoint, int damage)
    {
        Bullet bullet = UnityEngine.Object.Instantiate(
            bulletPrefab,
            firePoint,
            Quaternion.identity);
        bool wasLastShot = false;

        if (CurrentAmmo == 1)
        {
            bullet.GetComponent<SpriteRenderer>().color = Color.red;
            wasLastShot = true;
            damage += 5;
        }

        int totalDamage = damage + (CurrentAmmo == 1 ? 5 : 0);
        bullet.Init(direction, damage, wasLastShot);
        CurrentAmmo--;
        OnAmmoChanged?.Invoke(CurrentAmmo);

        if (wasLastShot)
        {
            Reload();
        }

        return wasLastShot;
    }

    public void Reload()
    {
        if (IsReloading)
        {
            return;
        }

        if (CurrentAmmo >= MaxAmmo)
        {
            return;
        }

        IsReloading = true;
        OnReloadStart?.Invoke();

        progressController.Begin(
            ReloadTime,
            () =>
            {
                CompleteReload();
                IsReloading = false;
                OnReloadEnd?.Invoke();
            });
    }

    private void CompleteReload()
    {
        CurrentAmmo = MaxAmmo;
    }
}
