using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class slotItem : MonoBehaviour, IPointerClickHandler
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

    public GameObject selectedShader;
    public bool isSelected;

    //private Inventory inventoryScript;
    /*
    void Start()
    {
        inventoryScript = GameObject.Find("InventoryBox").GetComponent<Inventory>();
    }
    */

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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Slot clicked!");
            onLeftClick();
        }
    }


    public void onLeftClick()
    {
        //inventoryScript.deselectSlot();
        Debug.Log("IS SELECTED");
        selectedShader.SetActive(true);
        isSelected = true;
    }
}
