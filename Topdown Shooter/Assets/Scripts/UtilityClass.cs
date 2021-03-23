using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityClass
{
    public static T FindChildGameObjectWithTag<T>(GameObject parentObject, string tag) where T : Component
    {
        Transform parentTransform = parentObject.transform;
        foreach (Transform child in parentTransform)
        {
            if (child.tag == tag)
            {
                return child.GetComponent<T>();
            }
        }
        return null;
    }

    public static GameObject FindChildGameObjectWithTag(GameObject parentObject, string tag) 
    {
        Transform parentTransform = parentObject.transform;
        foreach (Transform child in parentTransform)
        {
            if (child.tag == tag)
            {
                return child.gameObject;
            }
        }
        return null;
    }

    public static Gun FindGunWithId<T>(GameObject parentObject,int id) where T : Component
    {

        Transform parentTransform = parentObject.transform;


        foreach (Transform gunCandidate in parentTransform)
        {
            Debug.Log(gunCandidate.name);
            Gun gun = gunCandidate.GetComponent<Gun>();
            if (gun && gun.gunId == id)
            {
                return gun;
            }
        }
        return null;

    }
}
