using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyStates currentState;
    private Rigidbody2D enemyRb;

    

    void Start()
    {
        currentState = GetComponent<EnemyStates>();
        enemyRb = GetComponent<Rigidbody2D>();
    }

    public void die()
    {
        Gun activeGun = GetComponent<EnemyCombat>().getGun();

        GunManagement.instance.dropGun(activeGun,transform.position);
        

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
