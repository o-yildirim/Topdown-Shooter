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
        float animationLength = 0f;
        AnimationClip[] clips = doorAnimator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "DoorOpening")
            {
                animationLength = clip.length;
                break;
            }
        }

        yield return new WaitForSeconds(animationLength + 0.25f);

        if (isOpening)
        {
            globalLight.enabled =true;
            //elevatorLight.enabled = false;
           
        }
        else
        {
            //elevatorLight.enabled = true;
            yield return new WaitForSeconds(0.25f);
            globalLight.enabled = false;
            
        }
       
    }
}
