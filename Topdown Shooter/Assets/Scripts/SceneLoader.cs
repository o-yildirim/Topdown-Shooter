using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader:MonoBehaviour
{
    public static SceneLoader instance;

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

    public void loadSceneCall(int buildIndex)
    {
        StartCoroutine(loadScene(buildIndex));
    }
    public IEnumerator loadScene(int buildIndex)
    {
        FadeManager.instance.fadeOut();
        float fadeDuration = FadeManager.instance.getAnimationLength();
        yield return new WaitForSeconds(fadeDuration);

        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(buildIndex);
        while (!sceneLoad.isDone)
        {
            yield return null;
        }
        LevelController levelController = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelController>();
        if (levelController != null)
        {
            levelController.onLevelLoad();
        }

        FadeManager.instance.fadeIn();
        fadeDuration = FadeManager.instance.getAnimationLength();
        yield return new WaitForSeconds(fadeDuration);

    }

    public void loadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int sceneToLoad = currentSceneIndex + 1;
        PlayerPrefs.SetInt("SavedGameScene", sceneToLoad);
        loadSceneCall(sceneToLoad);
    }

    public void restartLevel()
    {
        GameController.instance.deathScreen.SetActive(false);
        Player.instance.assignStoredInfo();

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        loadSceneCall(currentSceneIndex);
    }
   
}
