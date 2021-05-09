using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform gunHolster;
    public Gun currentActiveGun;
    void Start()
    {
        currentActiveGun = UtilityClass.FindActiveGun(gunHolster.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.isGamePaused) return;

        if (Input.GetMouseButton(0))
        {
            currentActiveGun.fire();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            currentActiveGun.stopFire();
        }
    }



}
