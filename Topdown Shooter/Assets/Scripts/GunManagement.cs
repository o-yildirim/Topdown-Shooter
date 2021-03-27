using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManagement : MonoBehaviour
{
    public int currentActiveGunId;
    public GameObject holster;

    public static GunManagement instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentActiveGunId = UtilityClass.FindActiveGun(holster).gunId;
    }
    public void switchGun(int activateId)
    {
        Gun currentActive = UtilityClass.FindGunWithId<Gun>(holster, currentActiveGunId);
        Gun toActivate = UtilityClass.FindGunWithId<Gun>(holster, activateId);

        currentActive.gameObject.SetActive(false);
        toActivate.gameObject.SetActive(true);

        currentActiveGunId = activateId;
    }
}
