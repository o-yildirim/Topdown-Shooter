using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PickableGun : PickableObject
{
    public int gunId;
    public string gunName;
    public int ammo;

    public GameObject displayCanvas;
    private Text gunInfoText;

    private void Start()
    {
        gunInfoText = displayCanvas.GetComponentInChildren<Text>();
       
    }
    public override void displayInfo()
    {
        if (displayCanvas)
        {
            gunInfoText.text = gunName + "\n";
            gunInfoText.text += "Bullets: " + ammo;
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
        GunManagement.instance.switchGun(gunId);
        Destroy(transform.root.gameObject);
    }




}
