using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private EnemyStates currentState;
    private Rigidbody2D enemyRb;


    public Transform[] patrolPoints;
    public float zRotationOffset = 0.05f;
    public float distanceOffset = 0.1f;
    public float patrollingSpeed = 4f;
    public float turningSpeed = 3f;
    public float waitTime = 1f;
    private int currentPatrolPoint = 0;

    private float currentWaitTime = 0f;
    private float targetZ;
    public enum PatrolState { Idle, GoingToTarget, Rotating };

    public PatrolState patrolState;


    private EnemyPathfinding pathfinding;

    void Start()
    {
        currentState = GetComponent<EnemyStates>();
        enemyRb = GetComponent<Rigidbody2D>();


        pathfinding = GetComponent<EnemyPathfinding>();
       // if(currentState.state == EnemyStates.EnemyState.Patrolling)
       // {
       //     pathfinding.changePath(patrolPoints[currentPatrolPoint]);
       // }
    }

    private void Update()
    {

        if (currentState.state != EnemyStates.EnemyState.Patrolling) return;

        if (patrolState == PatrolState.Idle)
        {
            pathfinding.changePath(patrolPoints[currentPatrolPoint]);
            patrolState = PatrolState.GoingToTarget;
        }
        else if (patrolState == PatrolState.GoingToTarget)
        {
            checkIfReachedPatrolPoint();
        }

    }

    public void checkIfReachedPatrolPoint()
    {
        Vector3 enemyPos = enemyRb.position;
        Vector3 targetPos = patrolPoints[currentPatrolPoint].position;
        float distance = Vector3.Distance(enemyPos, targetPos);

        if (distance <= distanceOffset)
        {
            enemyRb.velocity = Vector2.zero;
            
            currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
            patrolState = PatrolState.Idle;
        }
    }

    /*
    if (currentState.state != EnemyStates.EnemyState.Patrolling) return;

    if (patrolState == PatrolState.Idle)
    {
        wait();
    }
    else if (patrolState == PatrolState.Rotating)
    {
        rotateToPath();
    }
    else if (patrolState == PatrolState.GoingToTarget)
    {
        checkIfReachedPatrolPoint();
    }


}


public void wait()
{
    currentWaitTime += Time.deltaTime;
    if (currentWaitTime >= waitTime)
    {
        currentWaitTime = 0f;

        calculateNextRotation();

        patrolState = PatrolState.Rotating;
    }
}

private void calculateNextRotation()
{
    Vector2 lookPosition = patrolPoints[currentPatrolPoint].position;
    Vector2 direction = lookPosition - (Vector2)transform.position;
    targetZ = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;
}

public void rotateToPath()
{
    Vector2 lookPosition = patrolPoints[currentPatrolPoint].position;
    Vector2 direction = lookPosition - (Vector2)transform.position;

    if (Mathf.Abs(Vector3.Angle(direction, transform.up)) <= zRotationOffset)
    {
        enemyRb.velocity = direction.normalized * patrollingSpeed;
        patrolState = PatrolState.GoingToTarget;
    }
    else
    {
        float nextZrotation = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetZ, turningSpeed * Time.deltaTime);   
        transform.rotation = Quaternion.AngleAxis(nextZrotation, Vector3.forward);

    }
}

public void checkIfReachedPatrolPoint()
{
    Vector3 enemyPos = enemyRb.position;
    Vector3 targetPos = patrolPoints[currentPatrolPoint].position;
    float distance = Vector3.Distance(enemyPos, targetPos);

    if (distance <= distanceOffset)
    {
        enemyRb.velocity = Vector2.zero;
        patrolState = PatrolState.Idle;
        currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
    }
}*/
}


