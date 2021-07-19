using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatLevel1 : MonoBehaviour
{
    public Transform spawnPoint;
    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = spawnPoint.position;
        FadeManager.instance.fadeIn();
        GameController.instance.isGamePaused = false;
    }


}
