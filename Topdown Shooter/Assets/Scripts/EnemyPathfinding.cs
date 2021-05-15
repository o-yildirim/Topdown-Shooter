using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPathfinding : MonoBehaviour
{
    private Transform target;
    public float moveSpeed = 300f;
    public float chaseMoveSpeed = 750f;
    public float nextWaypointDistance = 3f;

    private EnemyStates states;
    private EnemySight sight;
    public float rotationSpeed = 3f;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    public bool isStopped = false;

    private Seeker seeker;
    private Rigidbody2D enemyRigidbody;

    public float invokeRate = 0.5f;
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        states = GetComponent<EnemyStates>();
        InvokeRepeating("updatePath", 0f, invokeRate);       
    }

    void FixedUpdate()
    {
      /*  if (isStopped)
        {
            enemyRigidbody.velocity = Vector2.zero;
            return;
        }
        */
        if (path == null) return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector3 nextLookDirection = transform.up;
        if (states.state != EnemyStates.EnemyState.Agressive)
        {
            Vector2 velocity = enemyRigidbody.velocity;
            nextLookDirection = Vector3.MoveTowards(transform.up, velocity, rotationSpeed * Time.deltaTime);

        }
        else
        {
            if (target)
            {
                Vector2 directionVectorToPlayer = (Vector2)target.position - enemyRigidbody.position;
                nextLookDirection = Vector3.MoveTowards(transform.up, directionVectorToPlayer, rotationSpeed * Time.deltaTime);
            }
        }
        transform.up = nextLookDirection;

        if (isStopped)
        {
            enemyRigidbody.velocity = Vector2.zero;
            return;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - enemyRigidbody.position).normalized;
        Vector2 moveVector = direction * moveSpeed * Time.deltaTime;
        enemyRigidbody.velocity = moveVector;

        float distance = Vector2.Distance(enemyRigidbody.position,path.vectorPath[currentWaypoint]);
        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        
    }

    public void onPathComplete(Path generatedPath)
    {
        if (!generatedPath.error)
        {
            path = generatedPath;
            currentWaypoint = 0;
        }
    }


    public void updatePath()
    {
        if (seeker.IsDone())
        {
            if (target)
            {
                seeker.StartPath(enemyRigidbody.position, target.position, onPathComplete); //Start path bittigi anda onPathComplete i cagiriyor callback bu demek
            }       
        }
    }

    public void changePath(Transform target)
    {
        if (target)
        {         
            this.target = target;
            if (!IsInvoking("updatePath"))
            {
                InvokeRepeating("updatePath", 0f, invokeRate);
            }
        }
    }

    public void stop()
    {
        CancelInvoke("updatePath");
        isStopped = true;
        enemyRigidbody.velocity = Vector2.zero;

    }
}
