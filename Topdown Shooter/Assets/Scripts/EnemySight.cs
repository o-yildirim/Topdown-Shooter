using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public float sightAngle = 30f;
    public float maxSightDistance = 5f;


    private Transform player;
    private SpriteRenderer spRenderer;
    private EnemyStates state;
    private EnemyPatrol patrol;
    private Color defaultColor;
    [SerializeField]
    private LayerMask detectableLayers;
    private EnemyPathfinding pathfinding;
    public bool canSeePlayer = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spRenderer = GetComponent<SpriteRenderer>();
        state = GetComponent<EnemyStates>();
        defaultColor = spRenderer.color;
        pathfinding = GetComponent<EnemyPathfinding>();
        patrol = GetComponent<EnemyPatrol>();
    }

    void Update()
    {
        //debugAngles();
        //detectWithTan2();
        if (!canSeePlayer)
        {
            detectWithVectorAngle();
        }
        else
        {
            isPlayerLost();
        }
    }

    /*private void debugAngles()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 direction2 = player.position - transform.position;
            float vector3angle = 90f - Vector3.Angle(direction2, transform.right);
            float vector3angle2 = Vector3.Angle(direction2, transform.up);
            Debug.Log("Vector3.Angle = " + vector3angle2 + "  " + vector3angle2);

            Debug.Log("Atan2= " + ((Mathf.Atan2(direction2.x, direction2.y) * Mathf.Rad2Deg) + transform.eulerAngles.z));
        }
    }*/

    /* public void detectWithTan2()
      {
          Vector2 direction = player.position - transform.position;
          float angle = (Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg) + transform.eulerAngles.z;

          if (angle > -sightAngle / 2f && angle <= sightAngle / 2f)
          {

              RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxSightDistance, detectableLayers);
              //Debug.Log("Raycasting");
              if (hit)
              {
                  spRenderer.color = Color.black;
                  state.state = EnemyStates.EnemyState.Agressive;
                  // Debug.Log("Player seen");
              }
              else
              {
                  spRenderer.color = Color.white;
              }

          }
          else
          {
              spRenderer.color = Color.white;
          }
      }*/

    public void isPlayerLost()
    {
        if (player)
        {
            Vector2 direction = player.position - transform.position;
            float angle = Vector3.Angle(direction, transform.up);
            if (Mathf.Abs(angle) <= sightAngle / 2f)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxSightDistance, detectableLayers);

                if (!hit) return;

                else if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
                {
                    if (state.state == EnemyStates.EnemyState.Agressive)
                    {
                        state.state = EnemyStates.EnemyState.Chasing;
                        pathfinding.changePath(player);
                    }
                    else
                    {
                        state.state = EnemyStates.EnemyState.Patrolling;
                        pathfinding.moveSpeed = patrol.patrollingSpeed;
                        patrol.leadToNextPatrolPoint();
                    }
                    pathfinding.isStopped = false;
      
                    spRenderer.color = defaultColor;
                    canSeePlayer = false;
                }
            }
        }


    }
    public void detectWithVectorAngle()
    {
        /*if (player)
        {
            Vector2 direction = player.position - transform.position;
            float angle = Vector3.Angle(direction, transform.up);
            if (Mathf.Abs(angle) <= sightAngle / 2f)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxSightDistance, detectableLayers);
                if (hit)
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                    {
                        spRenderer.color = Color.black;
                        state.state = EnemyStates.EnemyState.Agressive;
                        pathfinding.changePath(player);
                        pathfinding.moveSpeed = pathfinding.chaseMoveSpeed;
                        canSeePlayer = true;
                    }
                }
                else
                {
                    state.state = EnemyStates.EnemyState.Patrolling;
                    pathfinding.isStopped = false;
                    patrol.leadToNextPatrolPoint();
                    spRenderer.color = defaultColor;
                    canSeePlayer = false;
                }

            }
            else
            {
                state.state = EnemyStates.EnemyState.Patrolling;
                pathfinding.isStopped = false;
                canSeePlayer = false;
                patrol.leadToNextPatrolPoint();
                spRenderer.color = defaultColor;
            }
        }*/


        if (player)
        {
            Vector2 direction = player.position - transform.position;
            float angle = Vector3.Angle(direction, transform.up);
            if (Mathf.Abs(angle) <= sightAngle / 2f)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxSightDistance, detectableLayers);
                if (hit)
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                    {
                        spRenderer.color = Color.black;
                        state.state = EnemyStates.EnemyState.Agressive;
                        pathfinding.changePath(player);
                        pathfinding.moveSpeed = pathfinding.chaseMoveSpeed;
                        canSeePlayer = true;
                    }
                }
            }
        }


    }

    private void OnDrawGizmos()
    {
        float zRotation = transform.eulerAngles.z;

        Vector3 leftRay = Quaternion.AngleAxis(-sightAngle / 2f, Vector3.forward) * transform.up;
        Vector3 rightRay = Quaternion.AngleAxis(sightAngle / 2f, Vector3.forward) * transform.up;
        Gizmos.DrawRay(transform.position, leftRay * maxSightDistance);
        Gizmos.DrawRay(transform.position, rightRay * maxSightDistance);
    }
}
