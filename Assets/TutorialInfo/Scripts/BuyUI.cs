using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyUI : MonoBehaviour
{
    public Inventory playerInventory;
    public slotItem[] buySlots;
    public TMP_Text priceText;
    private slotItem selectedSlot;

    private RectTransform openCooler;
    private Image[] imageVisibility;
    public bool isOpen;

    public Button buyButton;
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
        buyButton.onClick.AddListener(BuySelectedItem);
        priceText.text = "Select rod to buy";
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

    public void SelectSlot(slotItem slot)
    {
        selectedSlot = slot;
        if (selectedSlot != null && selectedSlot.quantity > 0)
        {
            float price = GetItemPrice(selectedSlot.itemName);
            priceText.text = $"Buy For {price} coins";
        }
    }

    private void BuySelectedItem()
    {
        if (selectedSlot != null && selectedSlot.quantity > 0)
        {
            float price = GetItemPrice(selectedSlot.itemName);
            if (coinManager.numCoins >= price)
            {
                // Attempt to add to player's inventory
                int remaining = AddToPlayerInventory(
                    selectedSlot.itemName,
                    1,  // Quantity to add
                    selectedSlot.itemSprite,
                    selectedSlot.itemDescription
                );

                if (remaining == 0) // Successfully added
                {
                    coinManager.UpdateCoins(-Mathf.RoundToInt(price));
                    selectedSlot.quantity--;

                    if (selectedSlot.quantity <= 0)
                    {
                        selectedSlot.itemName = "";
                        selectedSlot.itemSprite = null;
                        selectedSlot.itemDescription = "";
                    }
                    selectedSlot.UpdateUI();
                    deSelectSlots();
                }
                else
                {
                    priceText.text = "Inventory full!";
                }
            }
            else
            {
                priceText.text = "Not enough coins!";
            }
        }
    }

    private int AddToPlayerInventory(string itemName, int quantity, Sprite sprite, string description)
    {
        int remaining = quantity;

        // First try stacking with existing items
        foreach (slotItem slot in playerInventory.inventorySlot)
        {
            if (slot.quantity > 0 && slot.itemName == itemName)
            {
                remaining = slot.AddItem(itemName, remaining, sprite, description);
                if (remaining == 0) return 0;
            }
        }

        // Then try empty slots
        foreach (slotItem slot in playerInventory.inventorySlot)
        {
            if (slot.quantity == 0)
            {
                remaining = slot.AddItem(itemName, remaining, sprite, description);
                if (remaining == 0) return 0;
            }
        }

        return remaining;
    }

    private float GetItemPrice(string itemName)
    {
        // Prices for fishing rod
        if(itemName == "Silver Fishing Rod")
        {
            return 50.0f;
        }
        else
        {
            return 150.0f;
        }
    }

    public void ToggleBuyUI()
    {
        isOpen = !isOpen;
    }

    public void deSelectSlots()
    {
        foreach (slotItem slot in buySlots)
        {
            slot.selectedShader.SetActive(false);
            slot.isSelected = false;
        }
        priceText.text = "Select rod to buy";
        selectedSlot = null;
    }
}
