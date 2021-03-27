using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : MonoBehaviour
{


    public int passingTroughEnemies = 2;
    private int currentPassedThroughEnemies = 0;

    [SerializeField]
    private LayerMask hittableObjects;

    private Vector2 previousPos;

    public Rigidbody2D bulletRb;
    public GameObject explosionEffectPrefab;
    public GameObject bloodEffectPrefab;

    private void Start()
    {
        previousPos = bulletRb.position;
    }
    private void FixedUpdate()
    {
        Vector2 differenceBetweenPositions = bulletRb.position - previousPos;
        RaycastHit2D hit = Physics2D.Raycast(previousPos, differenceBetweenPositions, differenceBetweenPositions.magnitude * 2f, hittableObjects);
        if (hit)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Enemy hitEnemy = hit.collider.GetComponent<Enemy>();
                hitEnemy.die();
              
                GameObject bloodEffect = Instantiate(bloodEffectPrefab, hit.transform.position, Quaternion.LookRotation(transform.up));
                Destroy(bloodEffect, 0.8f);

                currentPassedThroughEnemies++;
                if(passingTroughEnemies <= currentPassedThroughEnemies)
                {
                    Destroy(gameObject);
                }

            }
            else
            {
                GameObject createdExplosionEffect = Instantiate(explosionEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(createdExplosionEffect, 0.8f);
                Destroy(gameObject);

            }
        }
        previousPos = bulletRb.position;

    }
}
