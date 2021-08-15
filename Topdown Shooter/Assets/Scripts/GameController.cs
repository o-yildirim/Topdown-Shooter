using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public bool isGamePaused = false;
    public GameObject pauseMenu;
    public GameObject deathScreen;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ConsoleManager.instance.consoleCanvas.activeSelf)
            {
                ConsoleManager.instance.closeConsole();
                return;
            }

            if (!isGamePaused)
            {
                pause();
                pauseMenu.SetActive(true);
            }
            else
            {             
                pauseMenu.SetActive(false);
                resume();
            }
            
        }   
    }

    public void pause()
    {

        isGamePaused = true;
        Time.timeScale = 0f;
    }

    public void resume()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }
        
    }

    public void quitToWindows()
    {
        Application.Quit();
    }

    public void returnToMenu()
    {
        resume();
        StartCoroutine(SceneLoader.instance.loadMainMenu());
    }

   public void displayDeathScreen()
    {
        isGamePaused = true;
        deathScreen.SetActive(true);
    }


}
