﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunManagement : MonoBehaviour
{
    
    public int currentActiveGunId = -1; //UNARMED
    public GameObject holster;
    private SpriteRenderer playerRenderer;
    private PlayerShooting playerShootingScript;

    public float minRotation = 0f;
    public float maxRotation = 360f;

    public float minDistanceToBody = 0.5f;
    public float maxDistanceToBody = 3f;

    public Image gunImageUI;
    public Text bulletTextUI;
    public GameObject gunInfoCanvas;
    public Sprite unarmedSprite;

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
        /*
        Gun currentActiveGun = UtilityClass.FindActiveGun(holster);


        if (currentActiveGunId)
        {
            currentActiveGunId = UtilityClass.FindActiveGun(holster).gunId;
            Gun currentActiveGun = UtilityClass.FindGunWithId<Gun>(holster, currentActiveGunId);
            playerShootingScript.gameObject.GetComponent<SpriteRenderer>().sprite = currentActiveGun.weaponSprite;
            gunImageUI.sprite = currentActiveGun.droppedOnFloorGun.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            bulletTextUI.text = currentActiveGun.bullets.ToString();
        }*/

        holster = UtilityClass.FindChildByName(Player.instance.transform, "GunHolster").gameObject;
        playerRenderer = Player.instance.GetComponent<SpriteRenderer>();
        playerShootingScript = Player.instance.GetComponent<PlayerShooting>();
    }
    

    public void switchGun(PickableGun gunToActivate)
    {
        Gun currentActive = UtilityClass.FindGunWithId<Gun>(holster, currentActiveGunId);
        Gun toActivate = UtilityClass.FindGunWithId<Gun>(holster, gunToActivate.gunId);

        if (currentActive)
        {
            currentActive.gameObject.SetActive(false);
            dropGun(currentActive, playerShootingScript.transform.position);
        }



        toActivate.bullets = gunToActivate.ammo;
        toActivate.gameObject.SetActive(true);



        playerRenderer.sprite = toActivate.weaponSprite;

        gunImageUI.sprite = toActivate.droppedOnFloorGun.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        bulletTextUI.text = toActivate.bullets.ToString();

        currentActiveGunId = gunToActivate.gunId;
        playerShootingScript.currentActiveGun = toActivate;

        gunInfoCanvas.SetActive(true);


    }
    
    public void switchGun(int activateId,int ammo)
    {
        Gun currentActive = UtilityClass.FindGunWithId<Gun>(holster, currentActiveGunId);
        Gun toActivate = UtilityClass.FindGunWithId<Gun>(holster, activateId);

        if (currentActive)
        {
            currentActive.gameObject.SetActive(false);
            dropGun(currentActive, playerShootingScript.transform.position);
        }


        toActivate.bullets = ammo;
        toActivate.gameObject.SetActive(true);
        playerRenderer.sprite = toActivate.weaponSprite;

        currentActiveGunId = activateId;
        gunImageUI.sprite = toActivate.droppedOnFloorGun.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        bulletTextUI.text = toActivate.bullets.ToString();

        currentActiveGunId = activateId;
        playerShootingScript.currentActiveGun = toActivate;

        gunInfoCanvas.SetActive(true);

    }
    
    public void dropGun(Gun activeGun, Vector3 position)
    {
        if (activeGun)
        {
            float xLocation = position.x + Random.Range(minDistanceToBody, maxDistanceToBody);
            float yLocation = position.y + Random.Range(minDistanceToBody, maxDistanceToBody);
            Vector3 dropPosition = new Vector3(xLocation, yLocation, position.z);

            float gunRotation = Random.Range(minRotation, maxRotation);

            activeGun.drop(dropPosition, gunRotation);
        }
    }

    public void switchToUnarmed()
    {
        Gun currentActive = UtilityClass.FindGunWithId<Gun>(holster, currentActiveGunId);
        if (currentActive)
        {
            currentActive.gameObject.SetActive(false);
        }

        playerShootingScript.currentActiveGun = null; 

        currentActiveGunId = -1;
        playerRenderer.sprite = unarmedSprite;
        gunInfoCanvas.SetActive(false);

    }
}
