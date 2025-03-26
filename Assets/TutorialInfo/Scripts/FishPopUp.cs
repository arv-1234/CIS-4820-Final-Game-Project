using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

// This class manages the fish capture notification
public class FishPopUp : MonoBehaviour {
    // Declare variables
    public bool exit;
    private TMP_Text caughtText, newText;
    private Image fishIcon;

    void Start() {
        // Initiate variables
        caughtText = transform.Find("CaughtText").GetComponent<TMP_Text>();
        newText = transform.Find("NewText").GetComponent<TMP_Text>();
        fishIcon = transform.Find("Fish").GetComponent<Image>();
    }

    void Update() {
        // Make the pop up fade in and out
        if (!exit) {
            StartCoroutine(fadeAnimation());
        }
    }

    // Reset the values, edit the text, and display if it's new
    public void reset(string fishName, bool isNew) {
        caughtText = transform.Find("CaughtText").GetComponent<TMP_Text>();
        newText = transform.Find("NewText").GetComponent<TMP_Text>();
        fishIcon = transform.Find("Fish").GetComponent<Image>();

        caughtText.canvasRenderer.SetAlpha(0F);
        newText.canvasRenderer.SetAlpha(0F);
        fishIcon.canvasRenderer.SetAlpha(0F);

        exit = false;

        caughtText.text = "Caught a " + fishName + "!";
        if (isNew) {
            newText.text = "New";
        } else {
            newText.text = "";
        }
    }

    IEnumerator fadeAnimation() {
        // Return true early to not keep the program waiting
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
    }
}
