using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyUI : MonoBehaviour
{

    public slotItem[] buySlots;

    private RectTransform openCooler;
    private Image[] imageVisibility;
    public bool isOpen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    }

    // Update is called once per frame
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
        //priceText.text = "Select item to sell";
    }
}
