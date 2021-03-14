using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private EnemyStates currentState;
    private Rigidbody2D enemyRb;

    public Transform[] patrolPoints;
    public float offset = 0.5f;
    public float patrollingSpeed = 4f;
    public float turningSpeed = 3f;
    public float waitTime = 1f;
    private int currentPatrolPoint = 0;
    [SerializeField]
    private float currentWaitTime = 0f;
    public enum PatrolState { Idle, GoingToTarget, Rotating };

    public PatrolState patrolState;

    void Start()
    {
        currentState = GetComponent<EnemyStates>();
        enemyRb = GetComponent<Rigidbody2D>();
        patrolState = PatrolState.Idle;
    }

    private void Update()
    {

        if (currentState.state != EnemyStates.EnemyState.Patrolling) return;

        if (patrolState == PatrolState.Idle)
        {
            wait();
        }
        else if (patrolState == PatrolState.Rotating)
        {
           // rotateToPath();
        }


    }


    public void wait()
    {
        currentWaitTime += Time.deltaTime;
        if (currentWaitTime >= waitTime)
        {
            currentWaitTime = 0f;
            patrolState = PatrolState.Rotating;
        }
    }

    public void rotateToPath()
    {
        Vector2 lookPosition = patrolPoints[currentPatrolPoint].position;
        Vector2 direction = lookPosition - (Vector2)transform.position;
        float zRotation = -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        float currentZ = transform.eulerAngles.z;

        if (currentZ != zRotation)
        {
            float nextZrotation = Mathf.MoveTowards(transform.eulerAngles.z, zRotation, turningSpeed * Time.deltaTime);
            transform.rotation = Quaternion.AngleAxis(nextZrotation, Vector3.forward);
            if(currentZ == zRotation)
            {
                patrolState = PatrolState.GoingToTarget;
            }
        }



    }
}

  /*  private void FixedUpdate()
    {
        //if (currentState.state != EnemyStates.EnemyState.Patrolling) return;

        /*if (patrolState == PatrolState.Idle)
        {
            currentWaitTime += Time.deltaTime;
            if(currentWaitTime >= waitTime)
            {
                Debug.Log("Gecti");
                currentWaitTime = 0f;
            }
        }

     

        Vector2 direction = ((Vector2)patrolPoints[currentPatrolPoint].position - enemyRb.position);

        enemyRb.velocity = direction.normalized * enemySpeed;
        if (Vector3.Distance(enemyRb.position, patrolPoints[currentPatrolPoint].position) <= offset)
        {
            float targetZ = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            enemyRb.velocity = Vector2.zero;
            if (enemyRb.transform.eulerAngles.z <= targetZ)
            {
                enemyRb.MoveRotation(targetZ);
            }
            else
            {
                currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
            }
        }
    }
}
    
    */
