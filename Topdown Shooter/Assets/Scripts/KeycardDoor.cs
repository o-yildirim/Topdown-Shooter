using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardDoor : MonoBehaviour
{
    public Animator doorAnimator;
    public void open()
    {
        doorAnimator.SetTrigger("open");
    }
}
