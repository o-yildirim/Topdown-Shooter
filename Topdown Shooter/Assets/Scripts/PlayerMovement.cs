using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private Vector2 movementVector;

    public float moveSpeed = 2f;
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
 
        float xMovement = Input.GetAxisRaw("Horizontal");
        float yMovement = Input.GetAxisRaw("Vertical");

        movementVector = new Vector2(xMovement, yMovement);
    }

    private void FixedUpdate()
    {

        if (GameController.instance.isGamePaused)
        {
            playerRb.velocity = Vector2.zero;
            return;

        }

        playerRb.velocity = movementVector.normalized * moveSpeed;
    }
}
