using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform player;
    private Vector3 offset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position-player.position;
    }

    void Update()
    {
        transform.position = player.position + offset;
    }
}
