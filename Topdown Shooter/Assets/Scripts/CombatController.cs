using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    private float enemyCount;
    public void increaseEnemy()
    {
        enemyCount++;
    }

    public void decreaseEnemy()
    {
        enemyCount--;
        if(enemyCount == 0)
        {
            Debug.Log("Stage clear");
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
