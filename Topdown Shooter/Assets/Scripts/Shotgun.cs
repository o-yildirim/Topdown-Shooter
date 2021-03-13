using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public int bulletCapacity;
    public float fireRatio = 0.5f;

    public int bulletsToFire;
    public float angleBetweenBullets;

    public float bulletSpeed;

    public GameObject shotgunBullet;
    public GameObject shotgunFireEffect;
    public Transform firePoint;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fire();
        }
    }

    public void fire()
    {
        float zRotation = firePoint.transform.eulerAngles.z - ((bulletsToFire-1)/2 * angleBetweenBullets);

        for (int i = 0; i < bulletsToFire; i++)
        {
            GameObject bulletCreated = Instantiate(shotgunBullet, firePoint.position, firePoint.rotation);    
            Rigidbody2D bulletRigidbody = bulletCreated.GetComponent<Rigidbody2D>();
            bulletCreated.transform.rotation = Quaternion.AngleAxis(zRotation, Vector3.forward);   
            bulletRigidbody.velocity = bulletCreated.transform.up * bulletSpeed;
            zRotation += angleBetweenBullets;
        
        }

        GameObject effect = Instantiate(shotgunFireEffect, firePoint.position, firePoint.rotation);      
        effect.transform.SetParent(gameObject.transform);
        effect.transform.localScale = Vector3.one;
        Destroy(effect, 0.8f);
    }
}
