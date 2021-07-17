using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableFlashlight : PickableObject
{

    public GameObject displayCanvas;
    public GameObject playerFlashlight;
 
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

    public override void onPickup()
    {
        playerFlashlight.SetActive(true);
        Destroy(gameObject);
    }
}
