using TMPro;
using UnityEngine;

public class CoinManager : TeamBehaviour
{
    public static CoinManager Instance { get; private set; }
    [Header("References")]
    [SerializeField] private TextMeshProUGUI coinText;
    private int coinCount = 0;
    protected override void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        base.Awake();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (coinText == null)
        {
            coinText = GameObject.Find("CoinText")?.GetComponent<TextMeshProUGUI>();
        }
    }

    public void AddCoins(int amount)
    {
        coinCount += amount;
        UpdateCoinUI();
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.SetText("Coins: " + coinCount);
        }
    }
}
