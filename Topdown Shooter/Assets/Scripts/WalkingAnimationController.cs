using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingAnimationController : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D playerRb;

    private void Start()
    {
       playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        resetAnimatorParameters();

        if(playerRb.velocity == Vector2.zero)
        {
            animator.SetBool("Idle", true);
        }
 
        else if (playerRb.velocity.normalized.y > 0f)
        {
            animator.SetBool("walkingForward", true);
         
        }
        else if (playerRb.velocity.normalized.y < 0f)
        {
            animator.SetBool("walkingBackwards", true);
        }

    }

    public void resetAnimatorParameters()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("walkingBackwards", false);
        animator.SetBool("walkingForward", false);
    }
}
