using UnityEngine;

public class Launch_Bobble : MonoBehaviour {  
    // Declare variables
    public GameObject rod;
    public GameObject bobble;
    public GameObject staticBobble;
    private GameObject throwBobble;

    private float launchVelocity = 0f;
    private float lineLength = 700f;

    private bool thrown = false;

    private Renderer visibility;
    private LineRenderer rodLine;
    
    void Start() {
        // Initiate variables
        visibility = staticBobble.GetComponent<Renderer>();
        rodLine = gameObject.AddComponent<LineRenderer>();

        rodLine.startWidth = 0.02f;
        rodLine.endWidth = 0.02f;
        rodLine.startColor = Color.white;
        rodLine.endColor = Color.white;
        rodLine.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
    }

    void Update() {
        // Connect the tip of the rod to the bobble
        if (thrown) {
            rodLine.SetPosition(0, rod.transform.position);
            rodLine.SetPosition(1, throwBobble.transform.position);
        } else {
            rodLine.SetPosition(0, rod.transform.position);
            rodLine.SetPosition(1, staticBobble.transform.position);
        }

        // Holding Left-Click: increases the launch velocity of the bobble
        if (Input.GetMouseButton(0) && !thrown && launchVelocity < 700f) {
            launchVelocity += 10f;
        }

        // Releasing Left-Click: throws or removes the bobble 
        if (Input.GetMouseButtonUp(0)) {
            // Check if the bobble was thrown
            if (thrown) {
                // The bobble returns to the rod
                thrown = false;

                // Destroy the thrown bobble
                Destroy(throwBobble);

                // Enable the static bobble (visible)
                visibility.enabled = true;
            } else {      
                // The bobble is being thrown
                thrown = true;

                // Disable the static bobble (invisible)
                visibility.enabled = false;

                // Create and throw the new rigid bobble (change collision detection so it stops falling through the floor)
                throwBobble = Instantiate(bobble, transform.position, transform.rotation);
                throwBobble.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                throwBobble.GetComponent<Rigidbody>().AddRelativeForce(new Vector3 (0, launchVelocity, 0));

                // Reset the launch velocity
                launchVelocity = 0f;
            }
        }
    }
}
