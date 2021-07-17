using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickableObject : MonoBehaviour
{
    public abstract void onPickup();
    public abstract void displayInfo();
    public abstract void hideInfo();
 
}
