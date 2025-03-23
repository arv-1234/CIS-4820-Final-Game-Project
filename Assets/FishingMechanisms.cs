using UnityEngine;
using System.Collections;

public class FishingMechanisms : MonoBehaviour {
    // Declare variables
    public GameObject prefabBobble, prefabFish;
    private GameObject rodTip, staticBobble, launchedBobble, launchedFish;
    private ParticleSystem splash;
    private Renderer visible;
    private LineRenderer rodLine;
    private float launchVelocity, minigameProgress;
    private bool launch, coroutineActive, canFish, pause, playingMinigame; 
    private FishManager fishManager;
    private Fish fish;

    [SerializeField]
    private FishingMinigame fs;
    
    void Start() {
        // Initiate variables
        rodTip = transform.Find("Bobble_Launcher").gameObject;
        staticBobble = transform.Find("Bobble (Static)").gameObject;

        visible = staticBobble.GetComponent<Renderer>();
        rodLine = gameObject.AddComponent<LineRenderer>();

        rodLine.startWidth = 0.02F;
        rodLine.endWidth = 0.02F;
        rodLine.startColor = Color.white;
        rodLine.endColor = Color.white;
        rodLine.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));

        launchVelocity = 0F;
        
        launch = false;
        coroutineActive = false;
        canFish = false;
        pause = false;
        playingMinigame = false;

        fishManager = new FishManager();
        fishManager.initialize();
    }

    void Update() {
        // No actions can be done while being in a fishing minigame
        if (!playingMinigame) {
            // Connect the tip of the rod to the bobble with a line
            if (launch) {
                rodLine.SetPosition(0, rodTip.transform.position);
                rodLine.SetPosition(1, launchedBobble.transform.position);

                // Wait for a fish to appear
                if (!coroutineActive) {
                    coroutineActive = true;
                    StartCoroutine(fishAppearance());
                }
            } else {
                rodLine.SetPosition(0, rodTip.transform.position);
                rodLine.SetPosition(1, staticBobble.transform.position);
            }
            
            // Different actions can be done depending on if a fish is biting or not
            if (canFish) {
                // Pressing/Holding Left-Click: starts the fishing minigame
                if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))) {
                    canFish = false;
                    coroutineActive = false;
                    Debug.Log("Attempting to capture the fish.");

                    // Play minigame to get the fish 
                    StartCoroutine(minigame());
                }
            } else {
                // Cooldown before launching a bobble again
                if (!pause) {
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
                            splash = launchedBobble.transform.Find("Watersplash").GetComponent<ParticleSystem>();

                            // Reset the launch velocity
                            launchVelocity = 0f;
                        }
                    }
                }
            }
        }
    }
    
    // Delay before changing water splashing particles to indicate that a fish is on the line or not
    IEnumerator fishAppearance() {
        // Fish on the Line!
		yield return new WaitForSeconds(Random.Range(3F, 10F));

        // Check if it's null from being destroyed early
        if (splash != null) {
            splash.Play();
            Debug.Log("Fish on the line, left-click to capture.");

            canFish = true;
            yield return new WaitForSeconds(Random.Range(3F, 5F));

            // Fish Escaped! This can only happen if the player decides to not begin capture
            if (canFish) {
                splash.Stop();
                Debug.Log("Fish left, cannot capture it anymore.");
                canFish = false;
                coroutineActive = false;
            }
        } else {
            coroutineActive = false;
        }
	}

    // Fishing cooldown after attempting to capture a fish
    IEnumerator fishingCooldown() {
        pause = true;
        yield return new WaitForSeconds(1F);
        pause = false;
        Debug.Log("Fishing cooldown ended.");
    }

    // Delay before destroying the fish model
    IEnumerator destroyFish(GameObject Fish) {
        yield return new WaitForSeconds(5F);
        Destroy(Fish);
        Debug.Log("Fish model destroyed.");
    }

    IEnumerator minigame() {
        // Stop Update() from running new events
        playingMinigame = true;

        // Start the minigame (where minigameProgress: 0 = in progress, 1 = won, 2 = lost)
        Debug.Log("Minigame.");
        minigameProgress = 0F;

        // Wait for the minigame to finish (won/lost)
        while (true) {
            minigameProgress = fs.getResult();
            if (minigameProgress == 1F) {
                // Decide which fish appeared & report to the terminal
                fish = fishManager.GetRandomFish();
                Debug.Log("Captured a " + fish.getName() + "!");

                // Fish springs out of the water
                launchedBobble.GetComponent<SphereCollider>().enabled = false;
                launchedFish = Instantiate(prefabFish, launchedBobble.transform.position + new Vector3(0, 0.3F, 0), transform.Find("Fish_Launcher").gameObject.transform.rotation);
                launchedFish.GetComponent<Rigidbody>().AddRelativeForce(new Vector3 (0, 500F, 0));

                // Fish is put into inventory

                
                // Delay before destroying the fish model
                StartCoroutine(destroyFish(launchedFish));
                break;
            } else if (minigameProgress == 2F) {
                Debug.Log("Fish escaped.");
                break;
            }
        }

        // Reset the bobble
        launch = false;
        Destroy(launchedBobble);
        visible.enabled = true;
        
        // Apply fishing cooldown
        StartCoroutine(fishingCooldown());
        
        // Allow Update() to run again
        playingMinigame = false;
        yield return null;
    }
}
