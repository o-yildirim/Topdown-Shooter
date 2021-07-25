using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MouseInteractableObject
{
    public GameObject infoCanvas;
    public Animator doorAnimator;
    private bool isOpen = false;
    private Transform player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //Kendisine ID alacak
    }

    public override void onRightClick()
    {
        if (isOpen) return;

        Vector3 direction = player.position - transform.position;
        float angle = Vector3.Angle(direction, transform.up);

   

        if (Mathf.Abs(angle) < 90f)
        {
            openDoor();
         
        }
        else
        {
            //Locked from the other side
        }
    }

    public override void displayInfo()
    {
        if (infoCanvas)
        {
            infoCanvas.SetActive(true);
        }
    }

    public override void hideInfo()
    {
        if (infoCanvas)
        {
            infoCanvas.SetActive(false);
        }
    }

    public void openDoor()
    {
        isOpen = true;
        doorAnimator.SetTrigger("openDoor");
        Destroy(infoCanvas);
    }

    private void OnDestroy()
    {
        //Kendisine ait olan id yi playerprefsden silecek
    }
}
