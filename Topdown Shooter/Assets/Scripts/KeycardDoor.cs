using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardDoor : MonoBehaviour
{
    public Animator doorAnimator;

    public void open()
    {
        doorAnimator.SetBool("open",true);
    }

    public void close()
    {
        doorAnimator.SetBool("open", false);
    }
}
