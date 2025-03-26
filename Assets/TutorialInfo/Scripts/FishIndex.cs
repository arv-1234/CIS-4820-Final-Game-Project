using UnityEngine;
using TMPro;
using UnityEngine.UI;

// This class toggles the Fish Index
public class FishIndex : MonoBehaviour {
    // Declare variables
    private TMP_Text fishName;
    private Image hiddenFishIcon;
    private RectTransform openBook;
    private bool isOpen;

    void Start() {
        // Initiate variables
        openBook = transform.Find("Background").GetComponent<RectTransform>();
        openBook.anchoredPosition = new Vector2(openBook.anchoredPosition.x, -536F);
        isOpen = false;
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
            // Move Up
            if (openBook.anchoredPosition.y <= 1F) {
                openBook.anchoredPosition = new Vector2(openBook.anchoredPosition.x, openBook.anchoredPosition.y + 10F);
            }
        } else {
            // Move Down
            if (openBook.anchoredPosition.y >= -458F) {
                openBook.anchoredPosition = new Vector2(openBook.anchoredPosition.x, openBook.anchoredPosition.y - 10F);
            }
        }
    }

    // Given the new fish's name, reveal it in the fish index
    public void revealFish(string search) {
        // Get the fish's text and image
        fishName = transform.Find("Background").Find(search).Find("Text").GetComponent<TMP_Text>();
        hiddenFishIcon = transform.Find("Background").Find(search).Find("ImageBlack").GetComponent<Image>();

        // Reveal the fish's name and image
        fishName.text = search;
        hiddenFishIcon.canvasRenderer.SetAlpha(0F);
    }
}
