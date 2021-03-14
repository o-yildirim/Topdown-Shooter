using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public float sightAngle = 30f;
    public float maxSightDistance = 5f;
    private Transform player;
    private SpriteRenderer renderer;
    private EnemyStates state;

    [SerializeField]
    private LayerMask detectableLayers;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        renderer = GetComponent<SpriteRenderer>();
        state = GetComponent<EnemyStates>();
    }

    // Update is called once per frame
    void Update()
    {


         if (Input.GetKeyDown(KeyCode.Space))
         {


             Vector2 direction2 = player.position - transform.position;
             Debug.Log("Vector3.Angle = " + Vector3.Angle(direction2, transform.right));
             Debug.Log("Atan2= " + Mathf.Atan2(direction2.x, direction2.y) * Mathf.Rad2Deg);
         }


        Vector2 direction = new Vector2(player.position.x - transform.position.x, player.position.y -transform.position.y);
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        if (angle > -sightAngle/2f && angle <= sightAngle/2f)
        {
                      
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxSightDistance, detectableLayers);
            //Debug.Log("Raycasting");
            if (hit)
            {
                renderer.color = Color.black;
                state.state = EnemyStates.EnemyState.Agressive;
                Debug.Log(state.state);
               // Debug.Log("Player seen");
            }
            else
            {
                renderer.color = Color.white;
            }
          
        }
        else
        {
            renderer.color = Color.white;
        }

    }

    private void OnDrawGizmos()
    {

        float zRotation = transform.eulerAngles.z;

       

        Vector3 leftRay =  Quaternion.AngleAxis(zRotation - sightAngle/2f, Vector3.forward) * transform.up;
        Vector3 rightRay = Quaternion.AngleAxis(zRotation + sightAngle/2f,Vector3.forward) * transform.up;

        //float hipotenus = Mathf.Sqrt((maxSightDistance * maxSightDistance) + Vector3.Distance(leftRay,rightRay)/2f );

        Gizmos.DrawRay(transform.position, leftRay  * maxSightDistance );
        Gizmos.DrawRay(transform.position, rightRay * maxSightDistance );
        //Gizmos.DrawLine(transform.position + leftRay  * hipotenus, transform.position + rightRay * hipotenus);
        //Gizmos.DrawWireSphere((leftRay + rightRay)/2f,maxSightDistance/2f);

    }
}
