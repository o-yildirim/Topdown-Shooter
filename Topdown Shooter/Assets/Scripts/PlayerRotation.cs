using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private Vector3 mousePos;
    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
     
    }
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        Vector3 direction = (Vector2) mousePos - playerRb.position;
        float zAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        playerRb.MoveRotation(zAngle-90f);
        
    }

}
