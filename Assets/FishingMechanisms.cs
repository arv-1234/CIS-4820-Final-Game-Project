using UnityEngine;
using System.Collections;

public class FishingMechanisms : MonoBehaviour {
    // Declare variables
    private GameObject rodTip;
    public GameObject prefabBobble;
    private GameObject staticBobble;
    private GameObject launchedBobble;
    
    //private ParticleSystem splash;
    
    private Renderer visible;
    private LineRenderer rodLine;

    private float launchVelocity;

    private bool launch;
    
    void Start() {
        // Initiate variables
        rodTip = transform.Find("Bobble_Launcher").gameObject;
        staticBobble = transform.Find("Bobble (Static)").gameObject;
        //splash = transform.Find("Watersplash").GetComponent<ParticleSystem>();

        visible = staticBobble.GetComponent<Renderer>();
        rodLine = gameObject.AddComponent<LineRenderer>();

        rodLine.startWidth = 0.02F;
        rodLine.endWidth = 0.02F;
        rodLine.startColor = Color.white;
        rodLine.endColor = Color.white;
        rodLine.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));

        launchVelocity = 0F;
        
        launch = false;
    }

    void Update() {
        // Connect the tip of the rod to the bobble
        if (launch) {
            rodLine.SetPosition(0, rodTip.transform.position);
            rodLine.SetPosition(1, launchedBobble.transform.position);

            // Wait for a fish to appear/disappear
            //StartCoroutine(fishAppears(Random.Range(3f, 10f)));
            //StartCoroutine(fishDisappears(Random.Range(3f, 5f)));
        } else {
            rodLine.SetPosition(0, rodTip.transform.position);
            rodLine.SetPosition(1, staticBobble.transform.position);
        }

        // Holding Left-Click: increases the launch velocity of the bobble
        if (Input.GetMouseButton(0) && !launch && launchVelocity < 700F) {
            launchVelocity += 5F;
        }

        // Releasing Left-Click: launches or discards the bobble 
        if (Input.GetMouseButtonUp(0)) {
            // Check if the bobble was launched
            if (launch) {
                // The bobble returns to the rod
                launch = false;

                // Destroy the launched bobble
                Destroy(launchedBobble);

                // Enable the static bobble (visible)
                visible.enabled = true;
            } else {      
                // The bobble is being launched
                launch = true;

                // Disable the static bobble (invisible)
                visible.enabled = false;

                // Create and launch the prefab bobble
                launchedBobble = Instantiate(prefabBobble, rodTip.transform.position, rodTip.transform.rotation);
                launchedBobble.GetComponent<Rigidbody>().AddRelativeForce(new Vector3 (0, launchVelocity, 0));

                // Reset the launch velocity
                launchVelocity = 0f;
            }
        }
    }
    

    // Delay before playing water splashes to indicate that a fish is on the line
    /*IEnumerator fishAppears(float seconds) {
		yield return new WaitForSeconds(seconds);
        splash.Play();
        Debug.Log("Fish on the Line, Begin Capture.");
	}
    
    // Delay before stopping water splashes to indicate that a fish ran away
    IEnumerator fishDisappears(float seconds) {
		yield return new WaitForSeconds(seconds);
        splash.Stop();
        Debug.Log("Fish Escaped, End Capture.");
	}*/
}

