using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public enum GunState {Idle,Firing,Cooling,Reloading};

    public GunState gunState = GunState.Idle;
    public int gunId;
    public string gunName;
   
    public float recoilRate;
    public float cooldownRate = 0.5f;
    public float recoilLimit;
  
    private float currentRecoil = 0f;

    public int bullets;

    public int bulletsToFire;
    public float angleBetweenBullets;

    public float fireRate = 1f; //1 firing per second 
    protected float nextFire = 0.0f;

    public float bulletSpeed;

    public GameObject bulletPrefab;
    public GameObject bullletFireEffectPrefab;

    public Sprite weaponSprite;
    public GameObject droppedOnFloorGun;

    public Transform firePoint;


    void Update()
    {

        if (GameController.instance.isGamePaused) return;

        
     
        if(gunState == GunState.Firing)
        {
            if (currentRecoil < recoilLimit)
            {
                currentRecoil += recoilRate;
            }
        }
        else if(gunState == GunState.Cooling)
        {
            cooldown();          
        }
       
    }

    public void cooldown()
    {
        if (currentRecoil > 0f)
        {
            currentRecoil = Mathf.MoveTowards(currentRecoil,0,cooldownRate * Time.deltaTime);
        }
        else
        {
            gunState = GunState.Idle;
        }
    }

    public virtual void fire()
    {
        if (bullets <= 0) return;

        if (Time.time < nextFire) return;
        nextFire = Time.time + fireRate;

        gunState = GunState.Firing;

        float randomRecoil = Random.Range(-currentRecoil, currentRecoil);
        float zRotation = firePoint.transform.eulerAngles.z - ((bulletsToFire-1)/2 * angleBetweenBullets) + randomRecoil;

        for (int i = 0; i < bulletsToFire; i++)
        {
            GameObject bulletCreated = Instantiate(bulletPrefab,firePoint.position, firePoint.rotation);    
            Rigidbody2D bulletRigidbody = bulletCreated.GetComponent<Rigidbody2D>();
            bulletCreated.transform.rotation = Quaternion.AngleAxis(zRotation, Vector3.forward);   
            bulletRigidbody.velocity = bulletCreated.transform.up * bulletSpeed;
            zRotation += angleBetweenBullets;
        
        }
        bullets--;
       

        GameObject effect = Instantiate(bullletFireEffectPrefab,firePoint.position, firePoint.rotation);      
        effect.transform.SetParent(gameObject.transform);
        effect.transform.localScale = Vector3.one;
        Destroy(effect, 0.2f);
    }

    public virtual void stopFire()
    {
        gunState = GunState.Cooling;
    }

    public void drop(Vector3 dropPosition,float rotation)
    {
        GameObject droppedGunPrefab = Instantiate(droppedOnFloorGun, dropPosition, Quaternion.identity);
        GameObject droppedGun = droppedGunPrefab.transform.GetChild(0).gameObject;
        droppedGun.transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        PickableGun droppedGunScript = droppedGun.GetComponent<PickableGun>();
        droppedGunScript.ammo = bullets;
    }
}
