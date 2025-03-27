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

        fishIcons = transform.GetComponentsInChildren<Image>();
        for (int i = 0; i < 8; i++) {
            fishIcons[i].enabled = false;
        }
    }

    void Update() {
        if (!exit) {
            // Make the pop up fade in and out
            StartCoroutine(fadeAnimation());
        }
    }

    // Make text+icon visible, reset values, edit the text, and activate the code
    public void reset(string fishName, bool isNew) {
        caughtText.enabled = true;
        newText.enabled = true;

        fishIcon = transform.Find(fishName).GetComponent<Image>();
        fishIcon.enabled = true;

        caughtText.canvasRenderer.SetAlpha(0F);
        newText.canvasRenderer.SetAlpha(0F);
        fishIcon.canvasRenderer.SetAlpha(0F);

        caughtText.text = "Caught a " + fishName + "!";
        if (isNew) {
            newText.text = "New";
        } else {
            newText.text = "";
        }

        exit = false;
    }

    IEnumerator fadeAnimation() {
        // Return true early to stop the code from constantly activating
        exit = true;

        // Fade in
        caughtText.CrossFadeAlpha(1, 0.5F, false);
        newText.CrossFadeAlpha(1, 0.5F, false);
        fishIcon.CrossFadeAlpha(1, 0.5F, false);
        
        // Wait for a few seconds
        yield return new WaitForSeconds(3F);

        // Fade out
        caughtText.CrossFadeAlpha(0, 0.5F, false);
        newText.CrossFadeAlpha(0, 0.5F, false);
        fishIcon.CrossFadeAlpha(0, 0.5F, false);
        
        // Make text+icon invisible again
        yield return new WaitForSeconds(0.5F);
        caughtText.enabled = false;
        newText.enabled = false;
        fishIcon.enabled = false;
    }
} 
