using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class ElevatorDoor : MonoBehaviour
{
    private Animator doorAnimator;
    private Light2D elevatorLight;
    public Light2D globalLight;

    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
        elevatorLight = transform.root.GetComponentInChildren<Light2D>();


        open();
      

    }
    public void open()
    {
        doorAnimator.SetBool("open", true);
        StartCoroutine(manageElevatorLight(true));
    }

    public void close()
    {
        doorAnimator.SetBool("open", false);
        StartCoroutine(manageElevatorLight(false));

    }

    public IEnumerator manageElevatorLight(bool isOpening)
    {
        float animationLength = doorAnimator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationLength);
        if (isOpening)
        {
            globalLight.enabled =true;
           
        }
        else
        {
            elevatorLight.enabled = true;
            yield return new WaitForSeconds(0.25f);
            globalLight.enabled = false;
            
        }
       
    }
}
