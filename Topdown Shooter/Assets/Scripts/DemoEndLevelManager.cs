using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoEndLevelManager : MonoBehaviour, LevelController
{
    public ElevatorDoor elevator;
    public Transform spawnPoint;
    public Animator confrontAnimator;
    public Transform villain;






    public void onLevelLoad()
    {
        GameController.instance.isGamePaused = true;
        Player.instance.gameObject.SetActive(true);
        Player.instance.transform.position = spawnPoint.position;
        Player.instance.transform.up = Vector2.down;

        StartCoroutine(startLevel());
       
    
    }
    public void endLevel() { }
    public IEnumerator startLevel()
    {
        yield return new WaitForSeconds(3f);
        CameraMovement.instance.switchTarget(villain);
        CameraMovement.instance.smoothTime = 1f;
        // Coroutine zoomOut = StartCoroutine(CameraMovement.instance.zoomOut(10f, 0.75f));
        //yield return zoomOut;
        CameraMovement.instance.zoomOutCall(9.2f, 0.75f);
     
        confrontAnimator.SetTrigger("TriggerConfront");
      


    }
}
