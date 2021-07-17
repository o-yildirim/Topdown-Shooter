using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class Level0 : MonoBehaviour
{
    public Light2D labLight;
    public Animator firstDoorAnimator;

    public Transform scientist;
    private Dialogue scientistDialogue;

    public Transform player;






    void Start()
    {
        StartCoroutine(startLevel());
    }

    public IEnumerator startLevel()
    {
        //Door opened and player is in lab
        while (!firstDoorAnimator.GetCurrentAnimatorStateInfo(0).IsName("DoorOpening")) yield return null;
        yield return new WaitForSeconds(0.15f);
        labLight.gameObject.SetActive(true);

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
        //Camera.main.GetComponent<CameraMovement>().targ
        scientistDialogue = scientist.GetComponent<Dialogue>();

        DialogueManager.instance.startDialogue(scientistDialogue);

        yield return null;
    }
}
