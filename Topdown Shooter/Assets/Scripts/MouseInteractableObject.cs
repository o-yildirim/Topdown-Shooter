using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MouseInteractableObject : MonoBehaviour
{
    public abstract void onRightClick();
    public abstract void displayInfo();
    public abstract void hideInfo();
 
}
