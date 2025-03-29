using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class slotItem : MonoBehaviour, IPointerClickHandler
{
    // Item Data
    public string itemName;
    public int quantity; // Initialized to 0 by default
    public Sprite itemSprite;
    public string itemDescription;

    // Stacking system
    private int maxStack = 10;
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;
    public GameObject selectedShader;

    // Description UI
    public Image itemDescImage;
    public TMP_Text itemDescNameText;
    public TMP_Text itemDescText;

    public bool isSelected;

    private Inventory playerInventory;

    void Start()
    {
        playerInventory = GameObject.Find("InventoryBox").GetComponent<Inventory>();
        UpdateUI(); // Initialize empty slot visuals
    }

    public int AddItem(string newName, int addQty, Sprite newSprite, string newDesc)
    {
        // If slot is empty
        if (quantity == 0)
        {
            itemName = newName;
            itemSprite = newSprite;
            itemDescription = newDesc;
            quantity = addQty;
            UpdateUI();
            return 0;
        }

        // If slot has matching item and space
        if (itemName == newName && quantity < maxStack)
        {
            int total = quantity + addQty;
            int remaining = Mathf.Max(total - maxStack, 0);
            quantity = Mathf.Min(total, maxStack);
            UpdateUI();
            return remaining;
        }

        // Can't add to this slot
        return addQty;
    }

    void UpdateUI()
    {
        // Always update image and text visibility
        itemImage.enabled = quantity > 0;
        itemImage.sprite = itemSprite;
        quantityText.text = quantity.ToString();
        quantityText.enabled = quantity > 0;
    }

    // Rest of the class remains unchanged
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            onLeftClick();
        }
    }

    public void onLeftClick()
    {
        playerInventory.deSelectSlot();
        selectedShader.SetActive(true);
        isSelected = true;

        itemDescNameText.text = itemName;
        itemDescText.text = itemDescription;
        itemDescImage.sprite = itemSprite;
    }
}