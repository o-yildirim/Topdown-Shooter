using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public int gunId;

    public int bulletCapacity;

    public int bulletsToFire;
    public float angleBetweenBullets;

    public float fireRate = 1f; //1 firing per second 
    private float nextFire = 0.0f;

    public float bulletSpeed;

    public GameObject bulletPrefab;
    public GameObject bullletFireEffectPrefab;

    public Transform firePoint;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            fire();

           // GameObject holster = UtilityClass.FindChildGameObjectWithTag(transform.root.gameObject, "GunHolster");
           // UtilityClass.FindGunWithId<Gun>(transform.root.gameObject, 1);
        }
    }

    public void fire()
    {
        float zRotation = firePoint.transform.eulerAngles.z - ((bulletsToFire-1)/2 * angleBetweenBullets);

        for (int i = 0; i < bulletsToFire; i++)
        {
            GameObject bulletCreated = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);    
            Rigidbody2D bulletRigidbody = bulletCreated.GetComponent<Rigidbody2D>();
            bulletCreated.transform.rotation = Quaternion.AngleAxis(zRotation, Vector3.forward);   
            bulletRigidbody.velocity = bulletCreated.transform.up * bulletSpeed;
            zRotation += angleBetweenBullets;
        
        }

        GameObject effect = Instantiate(bullletFireEffectPrefab, firePoint.position, firePoint.rotation);      
        effect.transform.SetParent(gameObject.transform);
        effect.transform.localScale = Vector3.one;
        Destroy(effect, 0.8f);
    }
}
