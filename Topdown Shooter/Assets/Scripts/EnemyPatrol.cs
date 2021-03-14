using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private EnemyStates currentState;
    private Rigidbody2D enemyRb;

    public Transform[] patrolPoints;
    public float offset = 0.5f;
    public float enemySpeed = 4f;
    private int currentPatrolPoint = 0;

    void Start()
    {
        currentState = GetComponent<EnemyStates>();
        enemyRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //if (currentState.state != EnemyStates.EnemyState.Patrolling) return;

        enemyRb.velocity = ((Vector2) patrolPoints[currentPatrolPoint].position - enemyRb.position).normalized * enemySpeed;
        if(Vector3.Distance(enemyRb.position,patrolPoints[currentPatrolPoint].position) <= offset)
        {
            currentPatrolPoint = (currentPatrolPoint+1) % patrolPoints.Length;
        }
    }

}
