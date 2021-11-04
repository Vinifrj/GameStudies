using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI = null;

    // Update is called once per frame
    void Update()
    {
        //Open pause menu on ESC key
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();  
            }
        }
    }

    //Close menu and resume game
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    //Open menu and pause game
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    //Return to main menu
    public void LoadMenu()
    {
        //Make sure that the game time is not paused
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    //Close Application
    public void QuitGame()
    {
        Application.Quit();
    }
}
