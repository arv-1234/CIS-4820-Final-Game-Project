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
    public string itemDescription;

    // item description slot 
    public Image itemDescImage;
    public TMP_Text itemDescNameText;
    public TMP_Text itemDescText;

    // Item Slot
    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;

    public GameObject selectedShader;
    public bool isSelected;

    private Inventory playerInventory;

    private void Start()
    {
        playerInventory = GameObject.Find("InventoryBox").GetComponent<Inventory>();
    }
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
        playerInventory.deSelectSlot();
        //inventoryScript.deselectSlot();
        Debug.Log("IS SELECTED");
        selectedShader.SetActive(true);
        isSelected = true;
    }
}