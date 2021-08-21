using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoEndLevelManager : MonoBehaviour, LevelController
{
    public ElevatorDoor elevator;
    public Transform spawnPoint;
    public Animator confrontAnimator;
    public Transform villain;
    public GameObject creditsCanvas;
    public Animator creditsAnimator;





    public void onLevelLoad()
    {
        GameController.instance.isGamePaused = true;
        Player.instance.gameObject.SetActive(true);
        Player.instance.transform.position = spawnPoint.position;
        Player.instance.transform.up = Vector2.down;

        StartCoroutine(startLevel());
       
    
    }
   
    public IEnumerator startLevel()
    {
        yield return new WaitForSeconds(3f);
        CameraMovement.instance.switchTarget(villain);
        CameraMovement.instance.smoothTime = 1f;
        CameraMovement.instance.zoomOutCall(9.2f, 0.75f);
     
        confrontAnimator.SetTrigger("TriggerConfront");


        float animationLength = 0f;
        AnimationClip[] clips = confrontAnimator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if(clip.name == "ConfrontingPlayerAnimation")
            {
                animationLength = clip.length;
                break;
            }
        }
       

        yield return new WaitForSeconds(animationLength + 1f);

        Dialogue villainDialogue = villain.GetComponent<Dialogue>();
        DialogueManager.instance.startDialogue(villainDialogue);

        while (DialogueManager.instance.isAnyDialogueActive())
        {
            yield return null;
        }
        GameController.instance.isGamePaused = true;

        //Change sprite, shoot player

        creditsCanvas.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        creditsAnimator.SetTrigger("PlayCredits");


        //endLevel();

    }

    public void endLevel() { }


}
