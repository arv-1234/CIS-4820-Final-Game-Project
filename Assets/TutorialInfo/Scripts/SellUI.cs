using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellUI : MonoBehaviour
{
    // Add inventory reference
    public Inventory playerInventory;
    public slotItem[] sellSlots; // Assign these in Inspector
    public TMP_Text priceText;
    private slotItem selectedSlot;

    private RectTransform openCooler;
    private Image[] imageVisibility;
    public bool isOpen;

    public Button sellButton;
    public coinTotal coinManager;


    void Start()
    {
        openCooler = transform.Find("Background").GetComponent<RectTransform>();
        openCooler.anchoredPosition = new Vector2(openCooler.anchoredPosition.x, -536F);
        isOpen = false;

        // Make the UI invisible
        imageVisibility = transform.Find("Background").GetComponentsInChildren<Image>();
        for (int i = 0; i < imageVisibility.Length; i++)
        {
            if (imageVisibility[i] != null)
            {
                imageVisibility[i].enabled = false;
            }
        }

        playerInventory = GameObject.Find("InventoryBox").GetComponent<Inventory>();
        coinManager = GameObject.Find("TotalCoins").GetComponent<coinTotal>();

        sellButton.onClick.AddListener(SellSelectedItem);

    }

    void Update()
    {
        
        if (isOpen)
        {
            // Make the UI visible
            for (int i = 0; i < imageVisibility.Length; i++)
            {
                if (imageVisibility[i] != null)
                {
                    imageVisibility[i].enabled = true;
                }
            }

            // Move Up
            if (openCooler.anchoredPosition.y <= 1F)
            {
                openCooler.anchoredPosition = new Vector2(openCooler.anchoredPosition.x, openCooler.anchoredPosition.y + 10F);
            }
        }
        else
        {
            // Move Down
            if (openCooler.anchoredPosition.y >= -558F)
            {
                openCooler.anchoredPosition = new Vector2(openCooler.anchoredPosition.x, openCooler.anchoredPosition.y - 10F);
            }
            else
            {
                // Make the UI invisible
                for (int i = 0; i < imageVisibility.Length; i++)
                {
                    if (imageVisibility[i] != null)
                    {
                        imageVisibility[i].enabled = false;
                    }
                }
            }
        }
    }

    public void SellSelectedItem()
    {
        if (selectedSlot != null && selectedSlot.quantity > 0)
        {
            // Calculate total value
            float totalValue = GetItemPrice(selectedSlot.itemName) * selectedSlot.quantity;

            // Update coins
            coinManager.UpdateCoins(Mathf.RoundToInt(totalValue));

            // Update inventory
            DeductFromInventory(selectedSlot.itemName, selectedSlot.quantity);

            // Refresh UI
            UpdateSellSlots();
            deSelectSlots();
        }
    }


    private void DeductFromInventory(string itemName, int quantity)
    {
        foreach (slotItem inventorySlot in playerInventory.inventorySlot)
        {
            if (inventorySlot.itemName == itemName)
            {
                inventorySlot.quantity -= quantity;
                if (inventorySlot.quantity <= 0)
                {
                    inventorySlot.itemName = "";
                    inventorySlot.itemSprite = null;
                }
                inventorySlot.UpdateUI();
                break;
            }
        }
    }

    public void UpdateSellSlots()
    {
        // Clear all sell slots directly
        foreach (slotItem slot in sellSlots)
        {
            slot.itemName = "";
            slot.quantity = 0;
            slot.itemSprite = null;
            slot.itemDescription = "";
            slot.UpdateUI(); // Force UI refresh
        }

        // Populate with current inventory
        int sellSlotIndex = 0;
        foreach (slotItem inventorySlot in playerInventory.inventorySlot)
        {
            if (inventorySlot.quantity > 0 && sellSlotIndex < sellSlots.Length)
            {
                sellSlots[sellSlotIndex].sellUI = this;
                // Directly copy values from inventory
                sellSlots[sellSlotIndex].itemName = inventorySlot.itemName;
                sellSlots[sellSlotIndex].quantity = inventorySlot.quantity;
                sellSlots[sellSlotIndex].itemSprite = inventorySlot.itemSprite;
                sellSlots[sellSlotIndex].itemDescription = inventorySlot.itemDescription;
                sellSlots[sellSlotIndex].UpdateUI();

                sellSlotIndex++;
            }
        }

        priceText.text = "Select item to sell";
    }

    public void deSelectSlots()
    {
        foreach (slotItem slot in sellSlots)
        {
            slot.selectedShader.SetActive(false);
            slot.isSelected = false;
        }
        priceText.text = "Select item to sell";
    }

    public void UpdatePriceDisplay(slotItem slot)
    {
        selectedSlot = slot;
        if (slot.quantity > 0)
        {
            float pricePerItem = GetItemPrice(slot.itemName);
            float totalValue = pricePerItem * slot.quantity;
            priceText.text = $"Sell For {totalValue} coins";
        }
    }
    private float GetItemPrice(string itemName)
    {
        // This should match your fish price logic
       if(itemName == "Moorish" || itemName == "Flying Fish" || itemName == "Beta Fish" || itemName == "Bass")
        {
            return 15.0f;
        }
       else if(itemName == "Turtle" || itemName == "Squid" || itemName == "Dolphin")
        {
            return 30.0f;
        }
       else
        {
            return 60.0f;
        }
    }

    public void ToggleSellUI()
    {
        isOpen = !isOpen;
        if (isOpen) UpdateSellSlots();
    }
}