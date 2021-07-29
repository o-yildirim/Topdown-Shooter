using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CombatLevel1 : MonoBehaviour, LevelController
{
    public Transform spawnPoint;
    public KeycardDoor elevatorDoor;
    public Light2D globalLight;

    public void onLevelLoad()
    {
        Player.instance.transform.position = spawnPoint.position;
        Player.instance.gameObject.SetActive(true);
        Player.instance.storePlayerInfo();
        //FadeManager.instance.fadeIn();
        GameController.instance.isGamePaused = false;
        StartCoroutine(startLevel());
    }

    public void endLevel()
    {

    }
    public IEnumerator startLevel()
    {
        //   yield return new WaitForSeconds(FadeManager.instance.getAnimationLength());
       
        elevatorDoor.open();
        elevatorDoor.transform.root.GetComponentInChildren<Light2D>().enabled = false;
        globalLight.enabled = true;
        yield return null;
    }
}
