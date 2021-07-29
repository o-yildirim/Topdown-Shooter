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
            //Debug.Log(gunCandidate.name);
            Gun gun = gunCandidate.GetComponent<Gun>();
            if (gun && gun.gunId == id)
            {
                return gun;
            }
        }
        return null;

    }


    public static Gun FindGunWithName<T>(GameObject parentObject, string name) where T : Component
    {
        Transform parentTransform = parentObject.transform;

        foreach (Transform gunCandidate in parentTransform)
        {
            Debug.Log(gunCandidate.name);
            Gun gun = gunCandidate.GetComponent<Gun>();
            if (gun && gun.gunName == name)
            {
                return gun;
            }
        }
        return null;

    }




    public static Gun FindActiveGun(GameObject parentObject)
    {
        Transform parentTransform = parentObject.transform;

        foreach (Transform gunCandidate in parentTransform)
        {

            Gun gun = gunCandidate.GetComponent<Gun>();
            if (gun && gun.gameObject.activeSelf)
            {
                return gun;
            }
        }
        return null;
    }

    public static Transform FindChildByName(Transform parent,string nameToFind)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform currentChild = parent.GetChild(i);
            if(currentChild.name == nameToFind)
            {
                return currentChild;
            }
        }

        return null;
    }

}
