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

    public int bulletCapacity;

    public int bulletsToFire;
    public float angleBetweenBullets;

    public float fireRate = 1f; //1 firing per second 
    private float nextFire = 0.0f;

    public float bulletSpeed;

    public GameObject bulletPrefab;
    public GameObject bullletFireEffectPrefab;

    public Sprite weaponSprite;


    public Transform firePoint;


    void Update()
    {

        if (GameController.instance.isGamePaused) return;

        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            fire();
            gunState = GunState.Firing;
            if (currentRecoil < recoilLimit)
            {
                currentRecoil += recoilRate;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            gunState = GunState.Cooling;          
        }



        if(gunState == GunState.Cooling)
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

    public void fire()
    {
        float randomRecoil = Random.Range(-currentRecoil, currentRecoil);
        float zRotation = firePoint.transform.eulerAngles.z - ((bulletsToFire-1)/2 * angleBetweenBullets) + randomRecoil;

        //playerRb.velocity = Vector2.zero;
        for (int i = 0; i < bulletsToFire; i++)
        {
            GameObject bulletCreated = Instantiate(bulletPrefab,firePoint.position, firePoint.rotation);    
            Rigidbody2D bulletRigidbody = bulletCreated.GetComponent<Rigidbody2D>();
            bulletCreated.transform.rotation = Quaternion.AngleAxis(zRotation, Vector3.forward);   
            bulletRigidbody.velocity = bulletCreated.transform.up * bulletSpeed;
            zRotation += angleBetweenBullets;
        
        }

        GameObject effect = Instantiate(bullletFireEffectPrefab,firePoint.position, firePoint.rotation);      
        effect.transform.SetParent(gameObject.transform);
        effect.transform.localScale = Vector3.one;
        Destroy(effect, 0.2f);
    }
}
