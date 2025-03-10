using UnityEngine;
using UnityEngine.UI; // or TMPro if using TextMeshPro

public class DisplayCoins : MonoBehaviour
{
    public CoinData coinData; // Reference the CoinData Scriptable Object
    public Text coinText; // Assign in the Inspector

    void Start()
    {
        // Update the UI with the coin count
        coinText.text = "Coins: " + coinData.totalCoins;
    }
}