using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  
    public void die()
    {    
        GameController.instance.displayDeathScreen();
        //Destroy(gameObject);
       // gameObject.SetActive(false);
    }
}
