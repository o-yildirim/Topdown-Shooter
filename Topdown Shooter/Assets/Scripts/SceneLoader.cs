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

    public void loadSceneFadeCall(int buildIndex)
    {
        StartCoroutine(loadSceneWithFadeEffects(buildIndex));
    }
    public IEnumerator loadSceneWithFadeEffects(int buildIndex)
    {
        FadeManager.instance.fadeOut();
        float fadeDuration = FadeManager.instance.getAnimationLength();
        yield return new WaitForSeconds(fadeDuration);

        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(buildIndex);
        while (!sceneLoad.isDone)
        {
            yield return null;
        }

        GameObject levelManager = GameObject.FindGameObjectWithTag("LevelManager");
        if (levelManager)
        {
            LevelController levelController = levelManager.GetComponent<LevelController>();
            if (levelController != null)
            {
                levelController.onLevelLoad();
            }
        }

        

        FadeManager.instance.fadeIn();
        fadeDuration = FadeManager.instance.getAnimationLength();
        yield return new WaitForSeconds(fadeDuration);

    }
    
    public void loadSceneDirectlyCall(int sceneIndex)
    {
        StartCoroutine(loadSceneDirectly(sceneIndex));
        
    }

    public IEnumerator loadSceneDirectly(int buildIndex)
    {
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

    }

    public void loadSceneSync(int currentSceneIndex)
    {
        SceneManager.LoadScene(currentSceneIndex);
        LevelController levelController = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelController>();
        if (levelController != null)
        {
            levelController.onLevelLoad();
        }
    }

    public void loadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int sceneToLoad = currentSceneIndex + 1;
        PlayerPrefs.SetInt("SavedGameScene", sceneToLoad);
        loadSceneFadeCall(sceneToLoad);
    }

    public void restartLevel()
    {


        GameController.instance.deathScreen.SetActive(false);

       
        Player.instance.assignStoredInfo();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        loadSceneSync(currentSceneIndex);
        
    }

    public IEnumerator loadMainMenu()
    {
        FadeManager.instance.fadeOut();
        float fadeDuration = FadeManager.instance.getAnimationLength();
        yield return new WaitForSeconds(fadeDuration);


        Player.instance.gameObject.SetActive(false);
        GunManagement.instance.gunInfoCanvas.SetActive(false);

        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(0);
        while (!sceneLoad.isDone)
        {
            yield return null;
        }

 
        FadeManager.instance.fadeIn();
        fadeDuration = FadeManager.instance.getAnimationLength();
        yield return new WaitForSeconds(fadeDuration);

        GameController.instance.resume();

    }

}
