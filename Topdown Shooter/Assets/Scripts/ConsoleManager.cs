using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConsoleManager : MonoBehaviour
{
    public GameObject consoleCanvas;
    private ConsoleCommands commands;
    private InputField consoleInput;
    private Text previousCommands;

    private bool consoleOpen = false;

    private void Start()
    {
        commands = consoleCanvas.GetComponentInChildren<ConsoleCommands>();
        consoleInput = consoleCanvas.GetComponentInChildren<InputField>();
        previousCommands = consoleCanvas.GetComponentInChildren<Text>();


    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
            Time.timeScale = 0f;
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
            Time.timeScale = 1f;
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
