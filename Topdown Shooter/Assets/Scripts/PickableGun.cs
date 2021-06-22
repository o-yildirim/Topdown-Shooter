using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PickableGun : MonoBehaviour
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
    public void display()
    {
        if (displayCanvas)
        {
            gunInfoText.text = gunName + "\n";
            gunInfoText.text += "Bullets: " + ammo;
            displayCanvas.SetActive(true);
        }
    }

    public void hide()
    {
        if (displayCanvas)
        {
            displayCanvas.SetActive(false);
        }
    }


}
