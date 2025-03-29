using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class slotItem : MonoBehaviour
{
    // Item Data
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;

    // Item Slot
    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;

    public void itemFish(string itemName, int quantity, Sprite itemSprite)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        isFull = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;

        Debug.Log("Sprite Name: " + itemSprite);

    }
 
}
