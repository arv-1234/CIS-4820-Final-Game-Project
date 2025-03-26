using UnityEngine;

// This class toggles the Inventory
public class Inventory : MonoBehaviour {
    // Declare variables
    private RectTransform openCooler;
    private bool isOpen;

    void Start() {
        // Initiate variables
        openCooler = transform.Find("Background").GetComponent<RectTransform>();
        openCooler.anchoredPosition = new Vector2(openCooler.anchoredPosition.x, -536F);
        isOpen = false;
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
            // Move Up
            if (openCooler.anchoredPosition.y <= 1F) {
                openCooler.anchoredPosition = new Vector2(openCooler.anchoredPosition.x, openCooler.anchoredPosition.y + 10F);
            }
        } else {
            // Move Down
            if (openCooler.anchoredPosition.y >= -458F) {
                openCooler.anchoredPosition = new Vector2(openCooler.anchoredPosition.x, openCooler.anchoredPosition.y - 10F);
            }
        }
    }
}
