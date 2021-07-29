using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player instance;

    public Sprite lyingOnBackSprite;
    public Sprite facedownSprite;

    private SpriteRenderer spRenderer;

    private Sprite storedSprite;
    private Gun storedGun;

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
        gameObject.SetActive(false);
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
        storedSprite = spRenderer.sprite;
        storedGun = UtilityClass.FindActiveGun(GunManagement.instance.holster);
    }

    public void assignStoredInfo()
    {
        spRenderer.sprite = storedSprite;
        if (storedGun)
        {
            Debug.Log("sa");
            GunManagement.instance.switchGun(storedGun.gunId,storedGun.bullets);
        }
        else
        {
            Debug.Log("as");
            GunManagement.instance.switchToUnarmed();
        }
    }


}
