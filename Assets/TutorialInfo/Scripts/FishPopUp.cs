using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

// This class manages the fish capture notification
public class FishPopUp : MonoBehaviour {
    // Declare variables
    public bool exit;
    private TMP_Text caughtText, newText;
    private Image[] fishIcons;
    private Image fishIcon;

    void Start() {
        // Initialize variables (make the code inactive and text+icon invisible at first)
        exit = true;

        caughtText = transform.Find("CaughtText").GetComponent<TMP_Text>();
        caughtText.enabled = false;

        newText = transform.Find("NewText").GetComponent<TMP_Text>();
        newText.enabled = false;

        fishIcons = GetComponentsInChildren<Image>();
        for (int i = 0; i < fishIcons.Length; i++) {
            fishIcons[i].enabled = false;
        }
    }

    void Update() {
        if (!exit) {
            // Make the pop up fade in and out
            StartCoroutine(fadeAnimation());
        }
    }

    // Reset the pop up
    public void reset(string fishName, bool isNew) {
        // Make the text and icon visible
        caughtText.enabled = true;
        newText.enabled = true;

        fishIcon = transform.Find(fishName).GetComponent<Image>();
        fishIcon.enabled = true;

        // Make the text and icon invisible by setting their alpha color to zero
        caughtText.canvasRenderer.SetAlpha(0F);
        newText.canvasRenderer.SetAlpha(0F);
        fishIcon.canvasRenderer.SetAlpha(0F);

        // Modify text
        caughtText.text = "Caught a " + fishName + "!";
        if (isNew) {
            newText.text = "New";
        } else {
            newText.text = "";
        }

        // Activate Update()
        exit = false;
    }

    IEnumerator fadeAnimation() {
        // Stop the code from constantly activating
        exit = true;

        // Fade the text and icon in
        caughtText.CrossFadeAlpha(1, 0.5F, false);
        newText.CrossFadeAlpha(1, 0.5F, false);
        fishIcon.CrossFadeAlpha(1, 0.5F, false);
        
        // Wait for a few seconds
        yield return new WaitForSeconds(3F);

        // Fade the text and icon out
        caughtText.CrossFadeAlpha(0, 0.5F, false);
        newText.CrossFadeAlpha(0, 0.5F, false);
        fishIcon.CrossFadeAlpha(0, 0.5F, false);
        
        // Make the text and icon invisible again
        yield return new WaitForSeconds(0.5F);
        caughtText.enabled = false;
        newText.enabled = false;
        fishIcon.enabled = false;
    }
} 
