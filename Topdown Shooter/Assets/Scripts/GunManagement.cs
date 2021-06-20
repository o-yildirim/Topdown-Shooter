using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManagement : MonoBehaviour
{
    public int currentActiveGunId;
    public GameObject holster;
    public SpriteRenderer playerRenderer;
    public PlayerShooting playerShootingScript;

    public float minRotation = 0f;
    public float maxRotation = 360f;

    public float minDistanceToBody = 0.5f;
    public float maxDistanceToBody = 3f;

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

        dropGun(currentActive,playerShootingScript.transform.position);

        toActivate.gameObject.SetActive(true);

        playerRenderer.sprite = toActivate.weaponSprite;

        currentActiveGunId = activateId;
        playerShootingScript.currentActiveGun = toActivate;

    }

    public void dropGun(Gun activeGun,Vector3 position)
    {
        float xLocation = position.x + Random.Range(minDistanceToBody, maxDistanceToBody);
        float yLocation = position.y + Random.Range(minDistanceToBody, maxDistanceToBody);
        Vector3 dropPosition = new Vector3(xLocation, yLocation, position.z);

        float gunRotation = Random.Range(minRotation, maxRotation);

        activeGun.drop(dropPosition, gunRotation);
    }
}
