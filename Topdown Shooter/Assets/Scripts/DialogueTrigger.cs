using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    public Dialogue dialogue;
    public GameObject dialogueActivateCanvas;
    private bool isPlayerInCollider = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            dialogueActivateCanvas.SetActive(true);
            isPlayerInCollider = true;
        }   
    }


    private void Update()
    {
        if (isPlayerInCollider)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogueActivateCanvas.SetActive(false);
                triggerDialogue();
            }
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            
            dialogueActivateCanvas.SetActive(false);
            isPlayerInCollider = false;
        }
    }

    public void triggerDialogue()
    {
        DialogueManager.instance.startDialogue(dialogue);
    }
}
