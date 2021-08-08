using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CombatLevel1 : MonoBehaviour, LevelController
{
    public Transform spawnPoint;
    public Light2D globalLight;

    public void onLevelLoad()
    {


        Player.instance.transform.position = spawnPoint.position;
        Player.instance.gameObject.SetActive(true);
        Player.instance.storePlayerInfo();


        globalLight.enabled = true;


        GameController.instance.isGamePaused = false;
      
        //StartCoroutine(startLevel());
    }

    public void endLevel() { }
    

    
    public IEnumerator startLevel()
    {
        yield return null;
    }
}
