using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class Level0 : MonoBehaviour, LevelController
{
    public GameObject labLight;

    public Animator firstDoorAnimator;

    public Transform scientist;
    private Dialogue scientistDialogue;

    public Transform player;

    public Transform elevatorDoor;
    public KeycardReader elevatorDoorKeycardReader;
    public Animator elevatorDoorAnimator;
    public GameObject elevatorLight;




    void Start()
    {
        onLevelLoad(); //TITLE EKRANI GELINCE BU START KALKACAK DIREKT
    }

    public void onLevelLoad()
    {
        StartCoroutine(startLevel());
    }
    public IEnumerator startLevel()
    {
        //Door opened and player is in lab
        while (!firstDoorAnimator.GetCurrentAnimatorStateInfo(0).IsName("DoorOpening")) yield return null;
        yield return new WaitForSeconds(0.15f);
        labLight.SetActive(true);

        while (!firstDoorAnimator.GetCurrentAnimatorStateInfo(0).IsName("DoorClosed")) yield return null;

        for (int i = 0; i < player.childCount; i++)
        {
            Transform child = player.GetChild(i);
            if(child.name == "PlayerFlashlight")
            {
                child.gameObject.SetActive(false);
                break;
            }
        }

        //Getting closer to the scientist
        while (Vector3.Distance(scientist.position, player.position) >= 5f) yield return null;
        scientistDialogue = scientist.GetComponent<Dialogue>();
        DialogueManager.instance.startDialogue(scientistDialogue);

        
        //Event happens when player gets close to the exit door.
        while (Vector3.Distance(player.position, elevatorDoor.position) >= 4f) yield return null;

        scientistDialogue.isMainDialogueDone = false;
        string[] nextSentence = new string[] { "/D0 Here... you... go!" };
        string[] nextRepeating = new string[] { "/D0 I don't know if there are any survivors left and if so, I am not sure they would be friendly.", "/D1 Good luck." };
        scientistDialogue.setDialogues(nextSentence);
        scientistDialogue.setRepeatingEndDialogue(nextRepeating);
        DialogueManager.instance.startDialogue(scientistDialogue);
        while (DialogueManager.instance.isAnyDialogueActive()) yield return null;
        yield return new WaitForSeconds(0.8f);
        elevatorDoorKeycardReader.openDoor();

        while (elevatorDoorAnimator.GetCurrentAnimatorStateInfo(0).IsName("DoorClosed")) yield return null;
        elevatorLight.SetActive(true);

        while (elevatorDoorAnimator.GetCurrentAnimatorStateInfo(0).IsName("DoorOpening")) yield return null;
        yield return new WaitForSeconds(1f);
        labLight.SetActive(false);
        elevatorDoorKeycardReader.gameObject.SetActive(false);

        FadeManager.instance.fadeOut();
        float fadeDuration = FadeManager.instance.getAnimationLength();
        yield return new WaitForSeconds(fadeDuration);

        endLevel();
    }

    public void endLevel()
    {   
        GameController.instance.isGamePaused = true;
        SceneLoader.instance.loadNextLevel();
    }
 

}
