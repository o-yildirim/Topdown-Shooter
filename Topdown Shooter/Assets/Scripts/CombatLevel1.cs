﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CombatLevel1 : MonoBehaviour, LevelController
{
    public Transform spawnPoint;
    public Light2D globalLight;
    public bool isLevelClear = false;

    public void onLevelLoad()
    {


        Player.instance.transform.position = spawnPoint.position;
        Player.instance.gameObject.SetActive(true);
        Player.instance.storePlayerInfo();


        globalLight.enabled = true;


        GameController.instance.isGamePaused = false;
      
        //StartCoroutine(startLevel());
    }

    public void endLevel()
    {
        GameController.instance.hideMessageBox();
        //Elevator falan kapanacak
    }
    
    public void levelCleared()
    {
        isLevelClear = true;
        GameController.instance.displayTextToPlayer("Level cleared!\n Get back to the elevator.");
    }
    
    public IEnumerator startLevel()
    {
        yield return null;
    }
}
