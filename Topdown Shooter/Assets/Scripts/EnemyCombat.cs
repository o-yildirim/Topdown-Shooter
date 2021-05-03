using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private EnemyStates currentState;
    public Transform targetPlayer;

    private Rigidbody2D enemyRb;
    public float turningSpeed = 150f;
    public float rotationOffset = 0.05f;

    public enum FightingType { Melee,Gun}
    public FightingType enemyType;

    private Transform attackPoint;

    void Start()
    {
        if(enemyType == FightingType.Gun)
        {
            attackPoint = UtilityClass.FindChildGameObjectWithTag<Transform>(gameObject, "Gun").GetChild(0);  
        }
        else if(enemyType == FightingType.Melee)
        {
            attackPoint = UtilityClass.FindChildGameObjectWithTag<Transform>(gameObject, "Melee").GetChild(0); //HENUZ YOK
        }

        currentState = GetComponent<EnemyStates>();
        enemyRb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
       if (currentState.state != EnemyStates.EnemyState.Agressive) return;


        Vector3 lookDirection = targetPlayer.position - transform.position;
        if (Mathf.Abs(Vector3.Angle(transform.up, lookDirection)) <= rotationOffset)
        {
            shoot();
        }
        else
        {
            Vector3 nextLookDirection = Vector3.MoveTowards(transform.up, lookDirection, turningSpeed * Time.deltaTime);
            transform.up = nextLookDirection;
        }
    }
    

    public void shoot()
    {
        Debug.Log("SA");
    }

   
}
