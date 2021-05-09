using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private EnemyStates currentState;
    public Transform targetPlayer;

    private Gun enemyGun;
    //private MeleeWeapon melee; BURALAR EKLENECEK SONRA

    private Rigidbody2D enemyRb;
    public float turningSpeed = 150f;
    public float rotationOffset = 0.05f;
    public float shootPlayerAngle = 5f;

    public enum FightingType { Melee,Gun}
    public FightingType enemyType;

    public float shootingRange = 15f;


    public Transform weapon;

    private EnemyPathfinding pathfinding;

    void Start()
    {
        if(enemyType == FightingType.Gun)
        {
            weapon = UtilityClass.FindChildGameObjectWithTag<Transform>(gameObject, "Gun");  //SHOTGUNUN FIRE POINTINI OTO CEKIYOR
            enemyGun = weapon.GetComponent<Gun>();
        }
        else if(enemyType == FightingType.Melee)
        {
            weapon = UtilityClass.FindChildGameObjectWithTag<Transform>(gameObject, "Melee"); //HENUZ YOK
        }
        

        currentState = GetComponent<EnemyStates>();
        enemyRb = GetComponent<Rigidbody2D>();
        pathfinding = GetComponent<EnemyPathfinding>();
        
    }


    void Update()
    {
        if (currentState.state != EnemyStates.EnemyState.Agressive) return;


        if (targetPlayer)
        {

            float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);

            Vector3 lookDirection = targetPlayer.position - transform.position;
            Debug.Log(distanceToPlayer);
            //Mathf.Abs(Vector3.Angle(transform.up, lookDirection)) <= rotationOffset && 

            

            if (Mathf.Abs(Vector3.Angle(transform.up, lookDirection)) <= shootPlayerAngle && distanceToPlayer <= shootingRange)
            {
                pathfinding.stop();
                shoot();
            }
            else
            {
                // Vector3 nextLookDirection = Vector3.MoveTowards(transform.up, lookDirection, turningSpeed * Time.deltaTime);
                // transform.up = nextLookDirection;

            }
        }
    }
    
    

    public void shoot()
    {
        enemyGun.fire();
    }

   
    

    
   
}
