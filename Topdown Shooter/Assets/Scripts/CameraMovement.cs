using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform player;
    private Vector3 offset;
    public float smoothTime = 0.3f;
    public float cameraPosRatioBetweenMouseAndPlayer = 2f;
    private Vector3 speedVector = Vector3.zero;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - player.position;
    }

    void Update()
    {
        if (player)
        {

            //transform.position = player.position;


            Vector3 newPos = player.position + ((player.position  + Camera.main.ScreenToWorldPoint(Input.mousePosition)) /cameraPosRatioBetweenMouseAndPlayer) ;
            newPos += offset;
            //transform.position = newPos + offset;
            transform.position = Vector3.SmoothDamp(transform.position, newPos, ref speedVector, smoothTime);
            Debug.Log(transform.position);
        }
    }
}
