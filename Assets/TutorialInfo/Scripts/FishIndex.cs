using UnityEngine;
using TMPro;
using UnityEngine.UI;

// This class toggles the Fish Index
public class FishIndex : MonoBehaviour {
    // Declare variables
    private TMP_Text fishName;
    private TMP_Text[] textVisibility;
    private Image hiddenFishIcon;
    private Image[] imageVisibility;
    private RectTransform openBook;
    private bool isOpen;

    void Start() {
        // Initiate variables
        openBook = transform.Find("Background").GetComponent<RectTransform>();
        openBook.anchoredPosition = new Vector2(openBook.anchoredPosition.x, -536F);
        isOpen = false;

        // Make the UI invisible
        imageVisibility = transform.Find("Background").GetComponentsInChildren<Image>();
        for (int i = 0; i < imageVisibility.Length; i++) {
            if (imageVisibility[i] != null) {
                imageVisibility[i].enabled = false;
            }
        }
        textVisibility = transform.Find("Background").GetComponentsInChildren<TMP_Text>();
        for (int i = 0; i < textVisibility.Length; i++) {
            textVisibility[i].enabled = false;
        }
    }

    void Update() {
        // Pressing TAB: opens or closes the Fish Index
        if (Input.GetKeyDown(KeyCode.Tab)) {
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
            for (int i = 0; i < textVisibility.Length; i++) {
                textVisibility[i].enabled = true;
            }

            // Move Up
            if (openBook.anchoredPosition.y <= 1F) {
                openBook.anchoredPosition = new Vector2(openBook.anchoredPosition.x, openBook.anchoredPosition.y + 10F);
            }
        } else {
            // Move Down
            if (openBook.anchoredPosition.y >= -558F) {
                openBook.anchoredPosition = new Vector2(openBook.anchoredPosition.x, openBook.anchoredPosition.y - 10F);
            } else {
                // Make the UI invisible
                for (int i = 0; i < imageVisibility.Length; i++) {
                    if (imageVisibility[i] != null) {
                        imageVisibility[i].enabled = false;
                    }
                }
                for (int i = 0; i < textVisibility.Length; i++) {
                    textVisibility[i].enabled = false;
                }
            }
        }
    }

    // Given the new fish's name, reveal it in the fish index
    public void revealFish(string fish) {
        // Get the fish's text and image
        fishName = transform.Find("Background").Find(fish).Find("Text").GetComponent<TMP_Text>();
        hiddenFishIcon = transform.Find("Background").Find(fish).Find("ImageBlack").GetComponent<Image>();

        // Reveal the fish's name and image
        fishName.text = fish;
        if (hiddenFishIcon != null) {
            Destroy(hiddenFishIcon);
        }
    }
}
