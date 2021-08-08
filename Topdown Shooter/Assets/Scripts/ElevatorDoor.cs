using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class ElevatorDoor : MonoBehaviour
{
    private Animator doorAnimator;
    private Light2D elevatorLight;

    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
        elevatorLight = transform.root.GetComponentInChildren<Light2D>();


        open();
        elevatorLight.enabled = false;

    }
    public void open()
    {
        doorAnimator.SetBool("open", true);
    }

    public void close()
    {
        doorAnimator.SetBool("open", false);
    }
}
