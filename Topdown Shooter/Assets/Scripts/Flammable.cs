using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : MonoBehaviour
{
    public int collisionRequiredToIgnite = 5;

    private void OnParticleCollision(GameObject other)
    {
        collisionRequiredToIgnite--;
        if (collisionRequiredToIgnite <= 0)
        {
            Enemy enemy = GetComponent<Enemy>();
            if (enemy)
            {
                enemy.die();
            }
        }
    }
    private void OnParticleTrigger()
    {
        Debug.Log("Trigger");
    }
}
