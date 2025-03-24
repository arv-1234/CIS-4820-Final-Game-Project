using UnityEngine;
using System.Collections;

// This class manages the UI for the fishing minigame
public class FishingMinigame : MonoBehaviour {
    // Declare variables
    private float result, speed;
    private RectTransform progressBar, catchBar, fishIcon;
    private bool changeDirection;
    public bool done;
    private int direction, newDirection;
    
    void Start() {
        // Initialize variables
        reset();
    }

    void Update() {
        // Once done, pause everything
        if (!done) {
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

            // Holding Left-Click: moves the light green square upwards, else it goes downwards
            if (Input.GetMouseButton(0)) {
                if (catchBar.anchoredPosition.y < 198) {
                    catchBar.anchoredPosition = new Vector2(catchBar.anchoredPosition.x, catchBar.anchoredPosition.y + 0.7F);
                }
            } else {
                if (catchBar.anchoredPosition.y > -196) {
                    catchBar.anchoredPosition = new Vector2(catchBar.anchoredPosition.x, catchBar.anchoredPosition.y - 0.7F);
                }
            }

            // If the fish is within the player's catch bar
            if ((catchBar.anchoredPosition.y < fishIcon.anchoredPosition.y + 60) && (catchBar.anchoredPosition.y > fishIcon.anchoredPosition.y - 60)) {
                // Progress Bar Increases
                if (progressBar.sizeDelta.y < 540) {
                    progressBar.sizeDelta = new Vector2(progressBar.sizeDelta.x, progressBar.sizeDelta.y + 0.5F);
                } else {
                    // Progress Bar is max, we win!
                    result = 1F;
                    done = true;
                }
            } else {
                // Progress Bar Decreases
                if (progressBar.sizeDelta.y > 0) {
                    progressBar.sizeDelta = new Vector2(progressBar.sizeDelta.x, progressBar.sizeDelta.y - 0.5F);
                } else {
                    // Progress Bar is min, we lost!
                    result = 2F;
                    done = true;
                }
            }
        }
    }

    // Delay before the fish switches directions
    IEnumerator fishDirection() {
        changeDirection = false;

        // Waits 0-4 seconds before changing the direction
        yield return new WaitForSeconds(Random.Range(0, 4));
        
        // The new direction cannot be the same as the old direction
        newDirection = Random.Range(0, 2);
        while (direction == newDirection) {
            newDirection = Random.Range(0, 2);
        }
        direction = newDirection;

        // Change the movement speed of the fish
        speed = Random.Range(1F, 10F) / 10F;

        changeDirection = true;
    }

    // Resets the values (to make the minigame replayable)
    public void reset() {
        result = 0F;
        progressBar = transform.Find("Background").Find("ProgressBar").GetComponent<RectTransform>();
        progressBar.sizeDelta = new Vector2(progressBar.sizeDelta.x, 149.09F);
        catchBar = transform.Find("Background").Find("CatchBar").GetComponent<RectTransform>();
        catchBar.anchoredPosition = new Vector2(catchBar.anchoredPosition.x, -195F);
        fishIcon = transform.Find("Background").Find("Fish").GetComponent<RectTransform>();
        fishIcon.anchoredPosition = new Vector2(catchBar.anchoredPosition.x, -159F);
        direction = Random.Range(0, 2);
        changeDirection = true;
        speed = Random.Range(1F, 10F) / 10F;
        done = false;
    } 

    // Returns the current result (0 = in progress, 1 = win, 2 = lost)
    public float getResult() {
        return result;
    }
}
