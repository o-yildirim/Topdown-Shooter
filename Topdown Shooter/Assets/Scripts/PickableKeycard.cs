using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableKeycard : MouseInteractableObject
{
    public KeycardReader keycardReader;
    public GameObject infoCanvas;

    public override void onRightClick()
    {
        keycardReader.acquireKeycard();
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
