using TMPro;
using UnityEngine;

public class coinTotal : MonoBehaviour
{
    public TMP_Text quantityText;
    public int numCoins = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        quantityText.text = $"Coins: {numCoins}";
    }
    public void UpdateCoins(int amount)
    {
        numCoins += amount;
        quantityText.text = $"Coins: {numCoins}";
    }


}
