using UnityEngine;

public class Sensor : TeamBehaviour
{
    [Header("Sensor Settings")]
    [SerializeField] private float checkRadius = 0.2f;
    [SerializeField] private LayerMask targetLayer;

    [Header("Debug")]
    [SerializeField] private bool _isDetected;

    public bool IsDetected => _isDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            _isDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            _isDetected = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _isDetected ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}