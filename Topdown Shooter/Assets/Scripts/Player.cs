using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player instance;

    public Sprite lyingOnBackSprite;
    public Sprite facedownSprite;

    private SpriteRenderer spRenderer;

    private Sprite currentSprite;
    private Gun gun;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        spRenderer = GetComponent<SpriteRenderer>();
    }

    public void die(Vector3 hitPoint)
    {
        Vector3 direction = hitPoint - transform.position;
        float angle = Vector3.Angle(direction, transform.up) - 45f;

        if (Mathf.Abs(angle) <= 90f)
        {
            spRenderer.sprite = lyingOnBackSprite;
        }
        else
        {
            spRenderer.sprite = facedownSprite;
        }
        GameController.instance.displayDeathScreen();
    }

    public void storePlayerInfo()
    {
        currentSprite = spRenderer.sprite;
        gun = UtilityClass.FindActiveGun(GunManagement.instance.holster);
    }

    public void assignStoredInfo()
    {
        spRenderer.sprite = currentSprite;
        if (gun)
        {
            Debug.Log("sa");
            GunManagement.instance.switchGun(gun.gunId,gun.bullets);
        }
        else
        {
            Debug.Log("as");
            GunManagement.instance.switchToUnarmed();
        }
    }


}
