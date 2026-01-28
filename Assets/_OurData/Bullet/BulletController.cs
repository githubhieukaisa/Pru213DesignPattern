using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class BulletController : TeamBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifeTime = 2f;
    // [SerializeField] private int damage = 1;

    [Header("Impact Effects")]
    [SerializeField] private LayerMask hitLayer;

    [Header("References")]
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Rigidbody2D rb;
    private Coroutine _deactivateCoroutine;
    private float _timer;
    private bool usePooling = true;
    public void SetUsePooling(bool value)
    {
        usePooling = value;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();

        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        if (trailRenderer == null)
            trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        _timer = 0;
        if (trailRenderer) trailRenderer.Clear();
        if (rb) rb.velocity = transform.right * speed;
    }

    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
    }

    protected void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > lifeTime)
        {
            ReturnToPool();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & hitLayer) != 0)
        {
            // Debug.Log($"Bullet hit: {other.name}");

            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        if (!usePooling)
        {
            Destroy(this.gameObject);
            return;
        }
        BulletPoolManager.Instance.ReturnToPool(this.gameObject);
    }
}