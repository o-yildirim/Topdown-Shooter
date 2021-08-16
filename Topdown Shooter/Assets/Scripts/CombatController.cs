using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    private float enemyCount;
    public CombatLevel1 levelManager;
    public void increaseEnemy()
    {
        enemyCount++;
    }

    public void decreaseEnemy()
    {
        enemyCount--;
        if(enemyCount == 0)
        {
            levelManager.levelCleared();
        }
    }
 
}
