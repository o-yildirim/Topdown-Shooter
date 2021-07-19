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
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(buildIndex);
        while (!sceneLoad.isDone)
        {
            yield return null;
        }

    }

    public  void loadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(loadScene(currentSceneIndex + 1));
    }

   
}
