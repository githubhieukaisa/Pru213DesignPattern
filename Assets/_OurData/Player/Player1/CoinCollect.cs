using UnityEngine;

public class CoinCollect : TeamBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int coinValue = 1;

    protected override void ResetValue()
    {
        base.ResetValue();
        playerLayer = LayerMask.GetMask("Player");
        coinValue = 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            CoinManager.Instance.AddCoins(coinValue);
            Destroy(gameObject);
        }
    }
}
