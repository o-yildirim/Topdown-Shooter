using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private EnemyStates currentState;
    private Transform targetPlayer;

    private EnemySight sight;
    private Gun enemyGun;
    //private MeleeWeapon melee; BURALAR EKLENECEK SONRA

    private Rigidbody2D enemyRb;
    public float turningSpeed = 150f;
    public float rotationOffset = 0.05f;
    public float shootPlayerAngle = 5f;

    public enum FightingType { Melee,Gun}
    public FightingType enemyType;

    public float shootingRange = 15f;


    //public Transform weapon;

    private EnemyPathfinding pathfinding;

    void Start()
    {
        if(enemyType == FightingType.Gun)
        {
            enemyGun = UtilityClass.FindActiveGun(gameObject);  //SHOTGUNUN FIRE POINTINI OTO CEKIYOR
            //Debug.Log(weapon.name);
            //enemyGun = weapon.GetComponent<Gun>();
        }
        else if(enemyType == FightingType.Melee)
        {
           // weapon = UtilityClass.FindChildGameObjectWithTag<Transform>(gameObject, "Melee"); //HENUZ YOK
        }


        targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;

        currentState = GetComponent<EnemyStates>();
        enemyRb = GetComponent<Rigidbody2D>();
        pathfinding = GetComponent<EnemyPathfinding>();
        sight = GetComponent<EnemySight>();
    }


    void Update()
    {
       // if (GameController.instance.isGamePaused) return;
        if (currentState.state != EnemyStates.EnemyState.Agressive && currentState.state != EnemyStates.EnemyState.Chasing) return;


        if (targetPlayer)
        {

            float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);
            Vector3 lookDirection = targetPlayer.position - transform.position;
         
            if (Mathf.Abs(Vector3.Angle(transform.up, lookDirection)) <= shootPlayerAngle && distanceToPlayer <= shootingRange)
            {
                if (sight.canSeePlayer)
                {
                    pathfinding.stop();
                    shoot();
                }
               
            }
          
        }
    }
    
    

    public void shoot()
    {
        enemyGun.fire();
    }

   public Gun getGun()
    {
        return enemyGun;
    }
    

    
   
}
