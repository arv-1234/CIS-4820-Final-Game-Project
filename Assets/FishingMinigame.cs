using UnityEngine;

// This class manages the fishing minigame UI
public class FishingMinigame : MonoBehaviour {
    // Declare variables
    private float result;
    
    void Start() {
        // Initialize variables
        result = 0F;
    }

    void Update() {
        // Function: Reset the fishing minigame values
        // CoRoutine: make the bar go up and down 
        // CoRoutine: If the bar is on the fish, the progress bar goes up, if not it goes down
    }

    // Returns the result (1 = win, 2 = lost)
    public float getResult() {
        result = 1F;
        return result;
    }
}
