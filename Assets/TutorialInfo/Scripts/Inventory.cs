using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class toggles the Inventory
public class Inventory : MonoBehaviour {
    // Declare variables
    private RectTransform openCooler;
    private Image[] imageVisibility;
    public bool isOpen;
    public slotItem [] inventorySlot;

    //private List<FishItem> items = new List<FishItem>();
    //private Dictionary<string, FishItem> itemDictionary = new Dictionary<string, FishItem>();

    void Start() {
        // Initiate variables
        openCooler = transform.Find("Background").GetComponent<RectTransform>();
        openCooler.anchoredPosition = new Vector2(openCooler.anchoredPosition.x, -536F);
        isOpen = false;

        // Make the UI invisible
        imageVisibility = transform.Find("Background").GetComponentsInChildren<Image>();
        for (int i = 0; i < imageVisibility.Length; i++) {
            if (imageVisibility[i] != null) {
                imageVisibility[i].enabled = false;
            }
        }
    }

    void Update() {
        // Pressing Q: opens or closes the Inventory
        if (Input.GetKeyDown(KeyCode.Q)) {
            if (isOpen) {
                isOpen = false;
            } else {
                isOpen = true;
            }
        }

        if (isOpen) {
            // Make the UI visible
            for (int i = 0; i < imageVisibility.Length; i++) {
                if (imageVisibility[i] != null) {
                    imageVisibility[i].enabled = true;
                }
            }

            // Move Up
            if (openCooler.anchoredPosition.y <= 1F) {
                openCooler.anchoredPosition = new Vector2(openCooler.anchoredPosition.x, openCooler.anchoredPosition.y + 10F);
            }
        } else {
            // Move Down
            if (openCooler.anchoredPosition.y >= -558F) {
                openCooler.anchoredPosition = new Vector2(openCooler.anchoredPosition.x, openCooler.anchoredPosition.y - 10F);
            } else {
                // Make the UI invisible
                for (int i = 0; i < imageVisibility.Length; i++) {
                    if (imageVisibility[i] != null) {
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

        for (int i = 0; i < inventorySlot.Length; i++)
        {
            if (inventorySlot[i].isFull == false)
            {
                inventorySlot[i].itemFish(slotFish.getFishName(), slotFish.getQuantity(), slotFish.getSprite(), slotFish.getDescription());
                return;
            }
        }
    }

    public Sprite getSprite(string fishName)
    {
        return Resources.Load<Sprite>(fishName);
    }

    public void deSelectSlot()
    {
        for (int i = 0; i < inventorySlot.Length; i++)
        {
            inventorySlot[i].selectedShader.SetActive(false);
            inventorySlot[i].isSelected = false;
        }
    }    
}
