using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyStates currentState;
    private Rigidbody2D enemyRb;

    public float minRotation = 0f;
    public float maxRotation = 360f;

    public float minDistanceToBody = 0.5f;
    public float maxDistanceToBody = 3f;

    void Start()
    {
        currentState = GetComponent<EnemyStates>();
        enemyRb = GetComponent<Rigidbody2D>();
    }

    public void die()
    {
        Gun activeGun = GetComponent<EnemyCombat>().getGun();
        float xLocation = transform.position.x + Random.Range(minDistanceToBody, maxDistanceToBody);
        float yLocation = transform.position.y + Random.Range(minDistanceToBody, maxDistanceToBody);
        Vector3 dropPosition = new Vector3(xLocation, yLocation, transform.position.z);

        float gunRotation = Random.Range(minRotation, maxRotation);
        activeGun.drop(dropPosition, gunRotation);
        

        var scripts = GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour script in scripts)
        {
            if(script != this)
            {
                script.enabled = false;
            }
        }

        Destroy(transform.parent.gameObject); //Şimdilik kalsın buraya sprite falan girerse
       // enemyRb.velocity = Vector2.zero;
       // currentState.state = EnemyStates.EnemyState.Dead;
    }

   
    public void becomeWounded()
    {
        //Başka fonksiyonlar gelebilir
        currentState.state = EnemyStates.EnemyState.Unconsious;
    }
   
}
