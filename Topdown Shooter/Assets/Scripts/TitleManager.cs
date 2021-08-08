using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleManager : MonoBehaviour
{
    public GameObject continueButton;
    private int lastSavedScene;
    void Start()
    {
        if (PlayerPrefs.HasKey("SavedGameScene"))
        {
            continueButton.SetActive(true);
            lastSavedScene = PlayerPrefs.GetInt("SavedGameScene");
        }
    }

    public void continueGame()
    {
        FadeManager.instance.fadeOut();
        SceneLoader.instance.loadSceneFadeCall(lastSavedScene);
   
    }
    public void startNewGame()
    {
        FadeManager.instance.fadeOut();
        PlayerPrefs.DeleteAll();
        SceneLoader.instance.loadNextLevel();
    }
    public void quit()
    {
        Application.Quit();
    }
  
 
}
