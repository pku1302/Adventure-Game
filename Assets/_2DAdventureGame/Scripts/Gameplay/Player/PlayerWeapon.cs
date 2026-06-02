using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    private WeaponData weaponData;

    [SerializeField]
    private AmmoBarUI ammoBarUI;

    public Weapon CurrentWeapon { get; private set; }
    public AudioSource audioSource;
    public AudioClip[] fireSFXs;
    public AudioClip criticalSFX;

    private Rigidbody2D rigidbody2d;
    private PlayerStats playerStats;
    private float attackTimer = 0f;
    private bool isSpeedUp = false;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        CurrentWeapon.OnReloadStart += GainBuff;
        CurrentWeapon.OnReloadEnd += CancelBuff;
        CurrentWeapon.OnAmmoChanged += ammoBarUI.ConsumeAmmo;
        CurrentWeapon.OnReloadEnd += ammoBarUI.Refill;
        ammoBarUI.Init(CurrentWeapon.MaxAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer >= 0f)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    public void Init(ProgressController progressController, PlayerStats playerStats)
    {
        CurrentWeapon = new Weapon(weaponData, progressController);
        this.playerStats = playerStats;
    }

    public void Fire(Vector2 direction)
    {
        if (attackTimer > 0f || CurrentWeapon.IsReloading || CurrentWeapon.CurrentAmmo <= 0)
        {
            return;
        }
        bool wasLastShot = CurrentWeapon.Fire(direction, rigidbody2d.position + Vector2.up * 0.1f, (int)playerStats.totalAttack);
        if (wasLastShot)
        {
            audioSource.PlayOneShot(criticalSFX);
        }
        else
        {
            AudioClip clip = fireSFXs[Random.Range(0, fireSFXs.Length)];
            audioSource.PlayOneShot(clip);
        }

        attackTimer = playerStats.attackSpeed;
    }

    private void GainBuff()
    {
        if (!isSpeedUp)
        {
            playerStats.GainSpeedBuff(1.0f);
            isSpeedUp = true;
        }
    }

    private void CancelBuff()
    {
        if (isSpeedUp)
        {
            playerStats.CancelSpeedBuff(1.0f);
            isSpeedUp = false;
        }
    }

    private void OnDestroy()
    {
        CancelBuff();
    }

    public void SetCoolTime()
    {
        attackTimer = 0f;
    }


}
