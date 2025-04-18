using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class toggles the Inventory
public class Inventory : MonoBehaviour
{
    // Declare variables
    private RectTransform openCooler;
    private Image[] imageVisibility;
    public bool isOpen;
    public slotItem[] inventorySlot;

    //private List<FishItem> items = new List<FishItem>();
    //private Dictionary<string, FishItem> itemDictionary = new Dictionary<string, FishItem>();

    void Start()
    {
        // Initiate variables
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
    }

    void Update()
    {
        // Pressing Q: opens or closes the Inventory
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isOpen)
            {
                isOpen = false;
            }
            else
            {
                isOpen = true;
            }
        }

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


    public void addFish(Fish fish)
    {
        Sprite fishSprite = getSprite(fish.getName());
        FishItem slotFish = new FishItem(fish, fishSprite);
        int leftOver = 1;

        // First try to stack with existing items
        foreach (slotItem slot in inventorySlot)
        {
            if (slot.quantity > 0 && slot.itemName == slotFish.getFishName())
            {
                leftOver = slot.AddItem(
                    slotFish.getFishName(),
                    leftOver,
                    slotFish.getSprite(),
                    slotFish.getDescription()
                );
                if (leftOver == 0) return;
            }
        }

        // Then try empty slots
        foreach (slotItem slot in inventorySlot)
        {
            if (slot.quantity == 0)
            {
                leftOver = slot.AddItem(
                    slotFish.getFishName(),
                    leftOver,
                    slotFish.getSprite(),
                    slotFish.getDescription()
                );
                if (leftOver == 0) return;
            }
        }

        // Only show warning if actually full
        if (leftOver > 0)
        {
            Debug.LogWarning($"Inventory full! Couldn't add {leftOver} {slotFish.getFishName()}");
        }
    }

   
    public Sprite getSprite(string fishName)
    {
        return Resources.Load<Sprite>(fishName+"Boxed");
    }

    public void deSelectSlot()
    {
        foreach (slotItem slot in inventorySlot)
        {
            slot.selectedShader.SetActive(false);
            slot.isSelected = false;
        }
    }
}
