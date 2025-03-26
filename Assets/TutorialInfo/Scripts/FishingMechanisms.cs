using UnityEngine;
using System.Collections;

// This class manages the entire fishing system
public class FishingMechanisms : MonoBehaviour {
    // Declare variables
    public GameObject prefabBobble, prefabFish;
    private GameObject rodTip, staticBobble, launchedBobble, launchedFish, minigameUI, popUpUI, fishIndexUI;
    private ParticleSystem splash;
    private Renderer visible;
    private LineRenderer rodLine;
    private float launchVelocity, minigameProgress;
    private bool launch, coroutineActive, canFish, pause, playingMinigame, cancel; 
    private FishManager fishManager;
    private Fish fish;
    private FishingMinigame minigameScript;
    private FishPopUp popUpScript;
    private FishIndex fishIndexScript;
    
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

        minigameUI = GameObject.Find("FishingMinigame");
        minigameScript = minigameUI.GetComponent<FishingMinigame>();
        minigameUI.SetActive(false);

        popUpUI = GameObject.Find("FishPopUp");
        popUpScript = popUpUI.GetComponent<FishPopUp>();
        popUpUI.SetActive(false);

        fishIndexUI = GameObject.Find("FishIndexBook");
        fishIndexScript = fishIndexUI.GetComponent<FishIndex>();
    }

    void Update() {
        // No actions can be done while in a fishing minigame
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
                // Cooldown before launching the bobble again
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
                            cancel = true;

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
    
    // Delay before changing the water particles to indicate that a fish is (or is not) on the line
    IEnumerator fishAppearance() {
        cancel = false;
		yield return new WaitForSeconds(Random.Range(3F, 9F));

        // Check if it's null and/or cancelled from being destroyed earlier
        if (splash != null && !cancel) {
            // Fish on the line
            splash.Play();
            Debug.Log("Fish on the line, left-click to capture.");

            canFish = true;
            yield return new WaitForSeconds(Random.Range(3F, 5F));

            // Fish escaped (player decided to not begin capture)
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

    // Deals with the visiblity, resetting, and results of the fishing minigame UI
    IEnumerator minigame() {
        // Stop Update() from running new events
        playingMinigame = true;
        Debug.Log("Minigame in progress.");
        
        // Make the fishing minigame UI visible, change the difficulty, and reset its values
        minigameUI.SetActive(true);
        fish = fishManager.getRandomFish();
        minigameScript.setDifficulty(fish.getRarity());
        minigameScript.reset();

        // Wait for the minigame to finish and get the result (in progress = 0, win = 1, lost = 2)
        yield return new WaitUntil(() => minigameScript.done);
        minigameProgress = minigameScript.getResult();
        
        if (minigameProgress == 1F) {
            // Report which fish appeared
            popUpUI.SetActive(true);
            popUpScript.reset(fish.getName(), fish.getIsNew());
            yield return new WaitUntil(() => popUpScript.done);

            // Add it to the fish index and set isNew value to false
            fishIndexScript.revealFish(fish.getName());
            fish.setIsNew();

            // Fish springs out of the water
            launchedBobble.GetComponent<SphereCollider>().enabled = false;
            launchedFish = Instantiate(prefabFish, launchedBobble.transform.position + new Vector3(0, 0.3F, 0), transform.Find("Fish_Launcher").gameObject.transform.rotation);
            launchedFish.GetComponent<Rigidbody>().AddRelativeForce(new Vector3 (0, 500F, 0));

            // Fish is put into inventory

            
            // Delay before destroying the fish model
            StartCoroutine(destroyFish(launchedFish));
        } else {
            Debug.Log("Fish escaped.");
        }

        // Deactivate the fishing minigame (invisible)
        minigameUI.SetActive(false);

        // Reset the bobble
        launch = false;
        Destroy(launchedBobble);
        visible.enabled = true;
        
        // Apply the fishing cooldown
        StartCoroutine(fishingCooldown());
        
        // Allow Update() to run again
        playingMinigame = false;
        yield return null;
    }
}
