using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : TeamBehaviour
{
    public static BulletPoolManager Instance { get; private set; }

    [Header("Pool Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int initialPoolSize = 20;

    private Queue<GameObject> _availableBullets = new Queue<GameObject>();

    protected override void Awake()
    {
        base.Awake();
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (bulletPrefab == null)
        {
            bulletPrefab = transform.Find("Bullet")?.gameObject;
            if (bulletPrefab != null) bulletPrefab.SetActive(false);
        }
    }

    private void Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewBullet();
        }
    }

    private GameObject CreateNewBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab, transform);
        newBullet.SetActive(false);

        _availableBullets.Enqueue(newBullet);
        return newBullet;
    }

    public GameObject Spawn(Vector3 position, Quaternion rotation)
    {
        GameObject bullet;

        if (_availableBullets.Count > 0)
        {
            bullet = _availableBullets.Dequeue();
        }
        else
        {
            bullet = CreateNewBullet();
        }

        bullet.transform.SetPositionAndRotation(position, rotation);
        bullet.SetActive(true);

        return bullet;
    }

    public void ReturnToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        _availableBullets.Enqueue(bullet);
    }
}