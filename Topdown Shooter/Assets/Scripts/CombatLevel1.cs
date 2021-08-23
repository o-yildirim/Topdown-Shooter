using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CombatLevel1 : MonoBehaviour, LevelController
{
    public Transform spawnPoint;
    public Light2D globalLight;
    public bool isLevelClear = false;
    public ElevatorDoor elevator;

    public void onLevelLoad()
    {


        Player.instance.transform.position = spawnPoint.position;
        Player.instance.gameObject.SetActive(true);
        Player.instance.storePlayerInfo();
        //CameraMovement.instance.repositionImmidately();


        GameController.instance.isGamePaused = false;
      
        //StartCoroutine(startLevel());
    }

    public void endLevel()
    {
        if (isLevelClear)
        {
            GameController.instance.hideMessageBox();
            GameController.instance.isGamePaused = true;
            StartCoroutine(endGameCoroutine());
        }
    }
    
    public void levelCleared()
    {
        isLevelClear = true;
        GameController.instance.displayTextToPlayer("Level cleared!\nGet back to the elevator.");
    }
    
    public IEnumerator startLevel()
    {
        yield return null;
    }

    public IEnumerator endGameCoroutine()
    {
        Animator elevatorAnimator = elevator.GetComponent<Animator>();
        elevator.close();
        float animationLength = elevatorAnimator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationLength + 0.5f);
        SceneLoader.instance.loadNextLevel();
    }

   
}
