using UnityEngine;
using System.Collections;

public class isFishing : MonoBehaviour {
    // Declare variables
    private Rigidbody bobblePhysics;
    private ParticleSystem splash;

    void Start() {
        // Initiate variables
        bobblePhysics = GetComponent<Rigidbody>();
        splash = transform.Find("Watersplash").GetComponent<ParticleSystem>();
    }

    // Function that checks if the bobble is touching the water
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "Water") {
            // Report this to the terminal
            Debug.Log("Bobble in Water, Begin Fishing.");
            
            // Stop the bobble from moving
            Destroy(bobblePhysics);

            // Wait for a fish to appear
            StartCoroutine(fishAppears(Random.Range(3f, 10f)));
        }
    }

    // Waits a couple seconds before playing a splash particle animation to indicate a fish is on the line
    IEnumerator fishAppears(float seconds) {
		yield return new WaitForSeconds(seconds);
        splash.Play();
        Debug.Log("Fish on the Line, Begin Capture.");
	}
}
