using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    public static FadeManager instance;
    public Animator fadeAnimator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }
    public void fadeOut()
    {
        fadeAnimator.SetBool("fadingOut",true);
    }

    public void fadeIn()
    {
        fadeAnimator.SetBool("fadingOut", false);
    }

    public float getAnimationLength()
    {
        return fadeAnimator.GetCurrentAnimatorStateInfo(0).length;
    }

}
