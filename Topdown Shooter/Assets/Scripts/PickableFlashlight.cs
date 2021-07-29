using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableFlashlight : MouseInteractableObject
{

    public GameObject displayCanvas;
    private GameObject playerFlashlight;

    private void Start()
    {
       playerFlashlight = UtilityClass.FindChildByName(Player.instance.transform, "PlayerFlashlight").gameObject;
    }
    public override void displayInfo()
    {
        if (displayCanvas)
        {
            displayCanvas.SetActive(true);
        }
    }

    public override void hideInfo()
    {
        if (displayCanvas)
        {
            displayCanvas.SetActive(false);
        }
    }

    public override void onRightClick()
    {
        if (playerFlashlight)
        {
            playerFlashlight.SetActive(true);
            Destroy(gameObject);
        }
    }
}
