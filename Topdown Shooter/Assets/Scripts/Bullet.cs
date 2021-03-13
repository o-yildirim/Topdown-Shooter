using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private LayerMask hittableObjects;
    private Vector2 previousPos;

    public Rigidbody2D bulletRb;


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
            Destroy(gameObject);
        }      
        previousPos = bulletRb.position;
        
    }
}
