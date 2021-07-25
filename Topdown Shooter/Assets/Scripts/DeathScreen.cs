using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameController.instance.restartLevel();
        }
    }
}
