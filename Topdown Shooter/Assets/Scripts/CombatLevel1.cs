using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CombatLevel1 : MonoBehaviour, LevelController
{
    public Transform spawnPoint;
    private KeycardDoor elevatorDoor;
    public Light2D globalLight;

    public void onLevelLoad()
    {
        Debug.Log("dfsdfskl");

        Player.instance.transform.position = spawnPoint.position;
        Player.instance.gameObject.SetActive(true);
        Player.instance.storePlayerInfo();


        elevatorDoor = GameObject.FindGameObjectWithTag("Elevator").GetComponentInChildren<KeycardDoor>();

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
