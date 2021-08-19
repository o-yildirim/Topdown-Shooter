using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform target;
    private Vector3 offset;
    public float smoothTime = 0.3f;
    public float maxCamDistanceToPlayer = 4f;
    private Vector3 speedVector = Vector3.zero;
    private float originalCamSize;
    private float originalSmoothTime;
    private Camera cam;

    public static CameraMovement instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        target = Player.instance.transform;
        offset = transform.position - target.position;
        cam = GetComponent<Camera>();
        originalCamSize = cam.orthographicSize;
        originalSmoothTime = smoothTime;
    }

    void Update()
    {

        if (GameController.instance.isGamePaused)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref speedVector, smoothTime);
            return;
        }

        if (target)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePos - target.position;

            //Vector3 newPos = Vector3.ClampMagnitude(player.position + (player.position + mousePos),maxCamDistanceToPlayer);
            Vector3 newPos = target.position + (direction.normalized * maxCamDistanceToPlayer);
            newPos += offset;
            newPos.z = offset.z;
            transform.position = Vector3.SmoothDamp(transform.position, newPos, ref speedVector, smoothTime);
        }
    }

    public void switchTarget(Transform newFocus)
    {
        target = newFocus;
        offset = new Vector3(0f,0f,cam.transform.position.z);
    }


    public IEnumerator zoomOut(float targetSize,float zoomRatio)
    {
        while(cam.orthographicSize < targetSize)
        {
            float newSize = Mathf.MoveTowards(cam.orthographicSize, targetSize, zoomRatio * Time.deltaTime);
            cam.orthographicSize = newSize;
            yield return null;
        }
    }

    public IEnumerator zoomIn(float targetSize, float zoomRatio)
    {
        while (cam.orthographicSize > targetSize)
        {
            float newSize = Mathf.MoveTowards(cam.orthographicSize, targetSize, zoomRatio * Time.deltaTime);
            cam.orthographicSize = newSize;
            yield return null;
        }
    }

    public void zoomInCall(float targetSize, float zoomRatio)
    {
        StartCoroutine(zoomIn(targetSize, zoomRatio));
    }

    public void zoomOutCall(float targetSize, float zoomRatio)
    {
        StartCoroutine(zoomOut(targetSize, zoomRatio));
    }
}
