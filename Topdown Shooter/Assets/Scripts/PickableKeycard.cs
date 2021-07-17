using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableKeycard : MouseInteractableObject
{
    public KeycardDoor linkedDoor;
    public GameObject infoCanvas;

    public override void onRightClick()
    {
        linkedDoor.unlock();
        Destroy(gameObject);
    }

    public override void displayInfo()
    {
        infoCanvas.SetActive(true);
    }

    public override void hideInfo()
    {
        infoCanvas.SetActive(false);
    }

}
