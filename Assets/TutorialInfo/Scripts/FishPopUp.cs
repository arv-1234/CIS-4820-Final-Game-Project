using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// This class manages the fish capture notification
public class FishPopUp : MonoBehaviour {
    // Declare variables
    public bool done;
    private Text caughtText, newText;

    void Start() {
        // Initiate variables
        caughtText = transform.Find("CaughtText").GetComponent<Text>();
        newText = transform.Find("NewText").GetComponent<Text>();

        done = false;
        caughtText.text = "Caught a [FishName]!";
        newText.text = "New";
    }

    void Update() {
        // "Animate" the pop up UI
        StartCoroutine(animationUI());
    }

    // Resets the values, edits the text, and displays if it's new
    public void reset(string fishName, bool isNew) {
        done = false;
        caughtText.text = "Caught a " + fishName + "!";
        if (isNew) {
            newText.text = "New";
        } else {
            newText.text = "";
        }
    }

    IEnumerator animationUI() {
        // Zoom in on the UI



        // Wait for 5 seconds
        yield return new WaitForSeconds(5F);

        // Zoom out on the UI
    }
}
