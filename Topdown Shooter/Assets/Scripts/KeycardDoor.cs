using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardDoor : MonoBehaviour
{
    public bool isLocked = true;

    public void unlock()
    {
        isLocked = false;
    }

    public void open()
    {
        //animator vs
    }
}
