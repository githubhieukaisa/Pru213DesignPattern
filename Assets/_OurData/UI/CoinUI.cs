using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinText;

    private void Start()
    {
        if (_coinText == null) _coinText = GetComponent<TextMeshProUGUI>();
        CoinManager.Instance.coinText = _coinText;
        CoinManager.Instance.AddCoins(0);
    }
}