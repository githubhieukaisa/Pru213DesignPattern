using System.Collections;
using UnityEngine;

public class PlayerAttack : TeamBehaviour
{
    [Header("References")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform body;

    [Header("Weapon Stats")]
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private int bulletsPerShot = 1;
    [SerializeField] private float accuracySpread = 5f;
    [SerializeField] private bool usePooling = true;

    [Header("Magazine System")]
    [SerializeField] private int maxAmmo = 30;
    [SerializeField] private float reloadTime = 1.5f;
    [SerializeField] private bool isInfiniteAmmo = false;

    [Header("Debug Info (Read Only)")]
    [SerializeField] private int currentAmmo;
    [SerializeField] private bool isReloading = false;
    [SerializeField] private float lastFireTime;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (firePoint == null)
            firePoint = transform.Find("Body")?.Find("RightArm")?.Find("FirePoint");

        if (firePoint == null)
            firePoint = transform;

        if (body == null)
            body = transform.Find("Body");
    }

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            TryShoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    private void TryShoot()
    {
        if (isReloading) return;
        if (Time.time < lastFireTime + fireRate) return;

        if (currentAmmo <= 0)
        {
            if (!isInfiniteAmmo)
            {
                StartCoroutine(ReloadCoroutine());
                return;
            }
        }

        Shoot();
        lastFireTime = Time.time;
    }

    private void Shoot()
    {
        if (!isInfiniteAmmo) currentAmmo--;

        for (int i = 0; i < bulletsPerShot; i++)
        {
            float randomSpread = Random.Range(-accuracySpread, accuracySpread);

            Quaternion spreadRotation = Quaternion.Euler(0, 0, randomSpread);

            Quaternion finalRotation = firePoint.rotation * spreadRotation * Quaternion.Euler(0, body.localScale.x < 0 ? 180 : 0, 0);

            if (!usePooling)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, finalRotation);
                BulletController bulletController = bullet.GetComponent<BulletController>();
                if (bulletController != null) bulletController.SetUsePooling(false);
            }
            else
            {
                BulletPoolManager.Instance.Spawn(firePoint.position, finalRotation);
            }
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        if (isReloading) yield break;
        if (currentAmmo == maxAmmo) yield break;

        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    public void UpgradeShotCount(int amount)
    {
        bulletsPerShot += amount;
    }

    public void UpgradeMagazineSize(int amount)
    {
        maxAmmo += amount;
        currentAmmo = maxAmmo;
    }

    public void UpgradeFireRate(float reductionAmount)
    {
        fireRate -= reductionAmount;
        fireRate = Mathf.Max(0.05f, fireRate);
    }
}