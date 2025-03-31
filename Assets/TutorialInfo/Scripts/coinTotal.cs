using TMPro;
using UnityEngine;

public class coinTotal : MonoBehaviour
{
    public TMP_Text quantityText;
    public int numCoins = 300;
   
    void Start()
    {
        quantityText.text = $"${numCoins}";
    }
    public void UpdateCoins(int amount)
    {
        numCoins += amount;
        quantityText.text = $"${numCoins}";
    }


}
