using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPathfinding : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 300f;
    public float nextWaypointDistance = 3f;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    private Seeker seeker;
    private Rigidbody2D enemyRigidbody;
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();

        InvokeRepeating("updatePath", 0f, 0.5f);       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
            seeker.StartPath(enemyRigidbody.position, target.position, onPathComplete); //Start path bittigi anda onPathComplete i cagiriyor callback bu demek
        }
    }
}
