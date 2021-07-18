using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class KeycardReader : MouseInteractableObject
{

    public Sprite lockedSprite;
    public Sprite unlockedSprite;
    public Light2D lockInfoLight;
    public KeycardDoor doorToOpen;

    public GameObject infoCanvas;

    private bool isKeyCardAcquired = false;
    public override void displayInfo()
    {
        if (infoCanvas)
        {
            Text infoText = infoCanvas.GetComponentInChildren<Text>();
            if (!isKeyCardAcquired)
            {
                infoText.text = "Keycard required";
            }
            else
            {
                infoText.text = "Use keycard";
            }

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
    public override void onRightClick()
    {
        if (!isKeyCardAcquired) return; //keycard bul falan diye uyarı verilebilir

        openDoor();

    }

    public void openDoor()
    {
        GetComponent<SpriteRenderer>().sprite = unlockedSprite;
        lockInfoLight.color = Color.green;
        doorToOpen.open();
        Destroy(infoCanvas);
    }

    public void lockDoor()
    {
        doorToOpen.close();
        GetComponent<SpriteRenderer>().sprite = lockedSprite;
        lockInfoLight.color = Color.red;
        
    }

    public void acquireKeycard()
    {
        isKeyCardAcquired = true;
    }
}
