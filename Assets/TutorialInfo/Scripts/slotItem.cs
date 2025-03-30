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

    public SellUI sellUI;
    private Inventory playerInventory;

    void Start()
    {
        playerInventory = GameObject.Find("InventoryBox").GetComponent<Inventory>();
        sellUI = GameObject.Find("SellScreen").GetComponent<SellUI>();
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

    public void UpdateUI()
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
        // Always deselect both systems
        if (sellUI != null) sellUI.deSelectSlots();
        if (playerInventory != null) playerInventory.deSelectSlot();

        // Handle selection
        selectedShader.SetActive(true);
        isSelected = true;

        // Update description
        itemDescNameText.text = itemName;
        itemDescText.text = itemDescription;
        itemDescImage.sprite = itemSprite;

        // Update price display if in sell screen
        if (sellUI != null && quantity > 0)
        {
            sellUI.UpdatePriceDisplay(this);
        }
    }
}