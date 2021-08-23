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
        Player.instance.gameObject.SetActive(false);
        CameraMovement.instance.switchTarget(Player.instance.transform);
        CameraMovement.instance.resetAllAttributes();
        GunManagement.instance.switchToUnarmed();

        GameController.instance.isGamePaused = true;
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
