using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    HingeJoint2D joint;
    void Start()
    {
        joint = GetComponent<HingeJoint2D>();
    }

   
}
