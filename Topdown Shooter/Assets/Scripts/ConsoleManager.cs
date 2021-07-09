using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConsoleManager : MonoBehaviour
{

    public static ConsoleManager instance;
    public GameObject consoleCanvas;
    private ConsoleCommands commands;
    private InputField consoleInput;
    private Text previousCommands;

    private bool consoleOpen = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        commands = consoleCanvas.GetComponentInChildren<ConsoleCommands>();
        consoleInput = consoleCanvas.GetComponentInChildren<InputField>();
        previousCommands = consoleCanvas.GetComponentInChildren<Text>();


    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            if (!consoleOpen)
            {
                openConsole();
            }
            else
            {
                closeConsole();
            }
        }
    }

    public void openConsole()
    {
        if (!consoleCanvas.activeSelf)
        {
            GameController.instance.pause();
            consoleCanvas.SetActive(true);
            consoleInput.Select();
            consoleOpen = true;
        }
    }

    public void closeConsole()
    {
        if (consoleCanvas.activeSelf)
        {
            consoleCanvas.SetActive(false);
            consoleOpen = false;
            GameController.instance.resume();
        }
    }


    public void commandEntered()
    {
        string command = consoleInput.text;
        consoleInput.text = "";
        string result = commands.executeCommand(command);
        previousCommands.text += result;
        consoleInput.ActivateInputField();
        consoleInput.Select();
    }

}
