using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// This class manages the UI for the fishing minigame
public class FishingMinigame : MonoBehaviour {
    // Declare variables
    private float result, speed, minSpeed, maxSpeed, minMaxFish, minCatch, maxCatch, rodStats;
    private RectTransform progressBar, catchBar, fishIcon;
    private Image[] imageVisability;
    private bool changeDirection;
    public bool exit;
    private int direction, newDirection, rarity;
    private GameObject rod;
    
    void Start() {
        // Initialize variables
        progressBar = transform.Find("Background").Find("ProgressBar").GetComponent<RectTransform>();
        catchBar = transform.Find("Background").Find("CatchBar").GetComponent<RectTransform>();
        fishIcon = transform.Find("Background").Find("Fish").GetComponent<RectTransform>();

        // Make them invisible
        imageVisability = transform.Find("Background").GetComponentsInChildren<Image>();
        for (int i = 0; i < imageVisability.Length; i++) {
            imageVisability[i].enabled = false;
        }

        // Make the code inactive
        exit = true;
    }

    void Update() {
        if (!exit) {
            // Changes the fish's direction
            if (changeDirection) {
                StartCoroutine(fishDirection());
            }

            // Moves the fish
            if (direction == 0) {
                // Up
                if (fishIcon.anchoredPosition.y < 248) {
                    fishIcon.anchoredPosition = new Vector2(fishIcon.anchoredPosition.x, fishIcon.anchoredPosition.y + speed);
                }
            } else if (direction == 1) {
                // Down
                if (fishIcon.anchoredPosition.y > -236) {
                    fishIcon.anchoredPosition = new Vector2(fishIcon.anchoredPosition.x, fishIcon.anchoredPosition.y - speed);
                }
            } else {
                // Still
            }

            // Holding Left-Click: moves the catch bar upwards, else it goes downwards
            if (Input.GetMouseButton(0)) {
                if (catchBar.anchoredPosition.y < maxCatch) {
                    catchBar.anchoredPosition = new Vector2(catchBar.anchoredPosition.x, catchBar.anchoredPosition.y + 1F);
                }
            } else {
                if (catchBar.anchoredPosition.y > minCatch) {
                    catchBar.anchoredPosition = new Vector2(catchBar.anchoredPosition.x, catchBar.anchoredPosition.y - 1F);
                }
            }

            // If the fish is within the player's catch bar
            if ((catchBar.anchoredPosition.y < fishIcon.anchoredPosition.y + minMaxFish) && (catchBar.anchoredPosition.y > fishIcon.anchoredPosition.y - minMaxFish)) {
                // Progress Bar Increases
                if (progressBar.sizeDelta.y < 540) {
                    progressBar.sizeDelta = new Vector2(progressBar.sizeDelta.x, progressBar.sizeDelta.y + 0.5F);
                } else {
                    // Progress Bar is at max (100%), we win!
                    result = 1F;
                    for (int i = 0; i < imageVisability.Length; i++) {
                        imageVisability[i].enabled = false;
                    }
                    exit = true;
                }
            } else {
                // Progress Bar Decreases
                if (progressBar.sizeDelta.y > 0) {
                    progressBar.sizeDelta = new Vector2(progressBar.sizeDelta.x, progressBar.sizeDelta.y - 0.5F);
                } else {
                    // Progress Bar is at min (0%), we lost!
                    result = 2F;
                    for (int i = 0; i < imageVisability.Length; i++) {
                        imageVisability[i].enabled = false;
                    }
                    exit = true;
                }
            }
        }
    }

    // Delay before switching the fish's direction
    IEnumerator fishDirection() {
        // So Update() doesn't constantly run this code
        changeDirection = false;

        // Waits 0-2 seconds before changing the direction
        yield return new WaitForSeconds(Random.Range(0, 3));
        
        // The new direction cannot be the same as the old direction
        newDirection = Random.Range(0, 3);
        while (direction == newDirection) {
            newDirection = Random.Range(0, 3);
        }
        direction = newDirection;

        // Change the movement speed of the fish
        speed = Random.Range(minSpeed, maxSpeed + 1F) / 10F;

        changeDirection = true;
    }

    // Resets the values (replayability) and makes them visible
    public void reset() {
        result = 0F;

        progressBar.sizeDelta = new Vector2(progressBar.sizeDelta.x, 149.09F);
        catchBar.anchoredPosition = new Vector2(catchBar.anchoredPosition.x, -195F);
        fishIcon.anchoredPosition = new Vector2(catchBar.anchoredPosition.x, -159F);

        direction = Random.Range(0, 3);

        changeDirection = true;

        speed = Random.Range(minSpeed, maxSpeed + 1F) / 10F;

        for (int i = 0; i < imageVisability.Length; i++) {
            imageVisability[i].enabled = true;
        }

        exit = false;
    } 

    // Returns the current result (1F = win, 2F = lost)
    public float getResult() {
        return result;
    }

    // Sets the difficulty depending on the fish rod & rarity (changes fish speed, catch bar size, and fish range)
    public void setDifficulty(int rarity) {
        rod = GameObject.Find("FishingRod");
        if (rod != null) {
            Debug.Log("Wooden Fishing Rod Found!");
            rodStats = 0F;
        } else {
            rod = GameObject.Find("Silver Fishing Rod");
            if (rod != null) { 
                Debug.Log("Silver Fishing Rod Found!");
                rodStats = 10F;
            } else {
                rod = GameObject.Find("Gold Fishing Rod");
                if (rod != null) { 
                    Debug.Log("Gold Fishing Rod Found!");
                    rodStats = 25F;
                } else {
                    Debug.Log("No Rod Found!");
                    rodStats = 0F;
                }
            }
        }

        if (rarity == 1) {
            minSpeed = 1F;
            maxSpeed = 5F;
            catchBar.sizeDelta = new Vector2(catchBar.sizeDelta.x, 142.82F + rodStats);
            minMaxFish = 60F + rodStats;
            minCatch = -196F + rodStats;
            maxCatch = 198F - rodStats;
        } else if (rarity == 2) {
            minSpeed = 4F;
            maxSpeed = 8F;
            catchBar.sizeDelta = new Vector2(catchBar.sizeDelta.x, 103.9F + rodStats);
            minMaxFish = 40F + rodStats;
            minCatch = -211F + rodStats;
            maxCatch = 215F - rodStats;
        } else {
            minSpeed = 8F;
            maxSpeed = 10F;
            catchBar.sizeDelta = new Vector2(catchBar.sizeDelta.x, 77F + rodStats);
            minMaxFish = 25F + rodStats;
            minCatch = -225F + rodStats;
            maxCatch = 230F - rodStats;
        }

        reset();
    }
}
