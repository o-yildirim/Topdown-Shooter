using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public bool isGamePaused = false;
    public GameObject pauseMenu;
    public GameObject deathScreen;
    public GameObject messageBoxParent;

    private Text messageBoxText;
    private Animator messageBoxAnimator;

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

    private void Start()
    {
        messageBoxAnimator = messageBoxParent.GetComponent<Animator>();
        messageBoxText = messageBoxParent.GetComponentInChildren<Text>();
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
        hideMessageBox();
        StartCoroutine(SceneLoader.instance.loadMainMenu());
    }

   public void displayDeathScreen()
    {
        isGamePaused = true;
        deathScreen.SetActive(true);
    }

    public void displayTextToPlayer(string message)
    {
        messageBoxText.text = "";

        messageBoxText.text = message;
        if (messageBoxAnimator)
        {
            messageBoxAnimator.SetBool("displayingMessage", true);
        }
    }

    public void hideMessageBox()
    {
        if (messageBoxAnimator)
        {
            messageBoxAnimator.SetBool("displayingMessage", false);
        }
       
    }

}
