using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform player;
    private Vector3 offset;
    public float smoothTime = 0.3f;
    public float maxCamDistanceToPlayer = 4f;
    private Vector3 speedVector = Vector3.zero;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - player.position;
    }

    void Update()
    {

        if (GameController.instance.isGamePaused)
        {
            transform.position = Vector3.SmoothDamp(transform.position, player.position + offset, ref speedVector, smoothTime);
            return;
        }

        if (player)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePos - player.position;

            //Vector3 newPos = Vector3.ClampMagnitude(player.position + (player.position + mousePos),maxCamDistanceToPlayer);
            Vector3 newPos = player.position + (direction.normalized * maxCamDistanceToPlayer);
            newPos += offset;
            transform.position = Vector3.SmoothDamp(transform.position, newPos, ref speedVector, smoothTime);
            //Debug.Log(transform.position);
        }
    }
}
