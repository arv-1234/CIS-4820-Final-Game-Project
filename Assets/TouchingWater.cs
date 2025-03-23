using UnityEngine;

public class TouchingWater : MonoBehaviour {
    // Declare variable
    private Rigidbody bobblePhysics;

    void Start() {
        // Initiate variable
        bobblePhysics = GetComponent<Rigidbody>();
    }

    // Function that checks if the bobble is touching the water
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Water")) {
            // Report this to the terminal
            Debug.Log("Bobble in Water, Begin Fishing Minigame.");
            
            // Stop the bobble from moving
            Destroy(bobblePhysics);
        }
    }
}
