﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class KeycardDoor : MonoBehaviour
{
    private Animator doorAnimator;

    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }
    public void open()
    {
        doorAnimator.SetBool("open",true);
    }

    public void close()
    {
        doorAnimator.SetBool("open", false);
    }
}
