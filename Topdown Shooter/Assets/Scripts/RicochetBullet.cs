using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetBullet : MonoBehaviour
{
    [SerializeField]
    private LayerMask hittableObjects;
    private Vector2 previousPos;

    public int ricochetCount;
    private int currentRicochet = 0;
    public float speedAfterRichochet;
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
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Player hitPlayer = hit.collider.GetComponent<Player>();
                hitPlayer.die();

                GameObject bloodEffect = Instantiate(bloodEffectPrefab, hit.transform.position, Quaternion.LookRotation(transform.up));
                Destroy(bloodEffect, 0.8f);
            }
            else
            {
                GameObject createdExplosionEffect = Instantiate(explosionEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(createdExplosionEffect, 0.8f);

                if(ricochetCount == currentRicochet)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Vector2 comingDirection = hit.point - previousPos;
                    Vector2 newVelocityVector = Vector2.Reflect(comingDirection,hit.normal);
                    bulletRb.velocity = newVelocityVector.normalized * speedAfterRichochet;
                    currentRicochet++;
                }

            }

        }
        previousPos = bulletRb.position;

    }
}
