﻿using System.Collections;
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

        float angle = Vector3.Angle(playerRb.velocity, transform.up);

        if(playerRb.velocity == Vector2.zero)
        {
            animator.SetBool("Idle", true);
        }
 
        else if (angle < 90f)
        {
            animator.SetBool("walkingForward", true);
         
        }
        else
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
