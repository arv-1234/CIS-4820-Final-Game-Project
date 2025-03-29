using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    private bool isPaused = false;

    // Start with the pause menu disabled
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        // Checks if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Pauses game and shows pause screen
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // Pause game 
        isPaused = true;
    }

    // Removes pause screen and resumes game
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // Resume game at normal speed
        isPaused = false;
    }

    // Quits game
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game");
    }
}
