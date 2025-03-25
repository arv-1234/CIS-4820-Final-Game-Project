using UnityEngine;
using System.Collections;

// This class manages the fish capture notification
public class FishPopUp : MonoBehaviour {
    // Declare Variables
    private string fishName;
    bool done;
    

    void Start() {
        done = false;
    }

    void Update() {
        // Move the pop up UI
        StartCoroutine(animationUI());
    }

    // Constructor: gets the fish name and edits the text
    public void FishPopup(string fishName) {
        this.fishName = fishName;
    }

    IEnumerator animationUI() {
        // Zoom in on the UI


        // Wait for 5 seconds
        yield return new WaitForSeconds(5F);

        // Zoom out on the UI
    }
}
