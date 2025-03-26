using UnityEngine;
using System.Collections;

// This class manages the UI for the fishing minigame
public class FishingMinigame : MonoBehaviour {
    // Declare variables
    private float result, speed, minSpeed, maxSpeed, minMaxFish, minCatch, maxCatch;
    private RectTransform progressBar, catchBar, fishIcon;
    private bool changeDirection;
    public bool exit;
    private int direction, newDirection, rarity;
    
    void Start() {
        // Initialize variables
        progressBar = transform.Find("Background").Find("ProgressBar").GetComponent<RectTransform>();
        catchBar = transform.Find("Background").Find("CatchBar").GetComponent<RectTransform>();
        fishIcon = transform.Find("Background").Find("Fish").GetComponent<RectTransform>();
    }

    void Update() {
        // Once the minigame is finished, exit
        if (!exit) {
            // Changes the fish's direction
            if (changeDirection) {
                StartCoroutine(fishDirection());
            }

            // Moves the fish
            if (direction == 0) {
                // Fish goes up
                if (fishIcon.anchoredPosition.y < 248) {
                    fishIcon.anchoredPosition = new Vector2(fishIcon.anchoredPosition.x, fishIcon.anchoredPosition.y + speed);
                }
            } else if (direction == 1) {
                // Fish goes down
                if (fishIcon.anchoredPosition.y > -236) {
                    fishIcon.anchoredPosition = new Vector2(fishIcon.anchoredPosition.x, fishIcon.anchoredPosition.y - speed);
                }
            } else {
                // Fish stands still
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
                    exit = true;
                }
            } else {
                // Progress Bar Decreases
                if (progressBar.sizeDelta.y > 0) {
                    progressBar.sizeDelta = new Vector2(progressBar.sizeDelta.x, progressBar.sizeDelta.y - 0.5F);
                } else {
                    // Progress Bar is at min (0%), we lost!
                    result = 2F;
                    exit = true;
                }
            }
        }
    }

    // Delay before switching the fish's directions
    IEnumerator fishDirection() {
        // Update() doesn't constantly run this code
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

    // Resets the values (replayability)
    public void reset() {
        result = 0F;

        progressBar = transform.Find("Background").Find("ProgressBar").GetComponent<RectTransform>();
        progressBar.sizeDelta = new Vector2(progressBar.sizeDelta.x, 149.09F);

        catchBar = transform.Find("Background").Find("CatchBar").GetComponent<RectTransform>();
        catchBar.anchoredPosition = new Vector2(catchBar.anchoredPosition.x, -195F);
        
        fishIcon = transform.Find("Background").Find("Fish").GetComponent<RectTransform>();
        fishIcon.anchoredPosition = new Vector2(catchBar.anchoredPosition.x, -159F);

        direction = Random.Range(0, 3);

        changeDirection = true;

        speed = Random.Range(minSpeed, maxSpeed + 1F) / 10F;

        exit = false;
    } 

    // Returns the current result (1F = win, 2F = lost)
    public float getResult() {
        return result;
    }

    // Sets the difficulty depending on the fish's rarity (changes fish speed, catch bar size, and fish range)
    public void setDifficulty(int rarity) {
        if (rarity == 1) {
            minSpeed = 1F;
            maxSpeed = 5F;
            catchBar = transform.Find("Background").Find("CatchBar").GetComponent<RectTransform>();
            catchBar.sizeDelta = new Vector2(catchBar.sizeDelta.x, 142.82F);
            minMaxFish = 60F;
            minCatch = -196F;
            maxCatch = 198F;
        } else if (rarity == 2) {
            minSpeed = 4F;
            maxSpeed = 8F;
            catchBar = transform.Find("Background").Find("CatchBar").GetComponent<RectTransform>();
            catchBar.sizeDelta = new Vector2(catchBar.sizeDelta.x, 103.9F);
            minMaxFish = 40F;
            minCatch = -211F;
            maxCatch = 215F;
        } else {
            minSpeed = 8F;
            maxSpeed = 10F;
            catchBar = transform.Find("Background").Find("CatchBar").GetComponent<RectTransform>();
            catchBar.sizeDelta = new Vector2(catchBar.sizeDelta.x, 77F);
            minMaxFish = 25F;
            minCatch = -225F;
            maxCatch = 230F;
        }

        reset();
    }
}
