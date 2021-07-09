using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public bool isGamePaused = false;
    public GameObject pauseMenu;
    public GameObject[] otherMenuCanvasses; 
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
            bool anyCanvasOpen = false;
            foreach (GameObject canvas in otherMenuCanvasses)
            {
                if (canvas.activeSelf)
                {
                    if (canvas.transform.CompareTag("DialogueCanvas"))
                    {              
                        DialogueManager.instance.StopAllCoroutines();
                        StartCoroutine(DialogueManager.instance.endDialogue());                     
                    }
                    else if (canvas.transform.CompareTag("ConsoleCanvas"))
                    {
                        ConsoleManager.instance.closeConsole();
                    }
                    anyCanvasOpen = true;
                }
            }


            if (anyCanvasOpen) return;


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
        //Load scene 0
        Debug.Log("Returning to menu");
    }

    /*
    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
            Debug.Log("e.keyCode: " + e.keyCode);
    }*/

}
