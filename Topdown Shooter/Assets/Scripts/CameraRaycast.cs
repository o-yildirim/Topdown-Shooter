using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    private PickableObject pickableObjectOnTheFloor;
    private Camera cam;
    public float pickUpDistance = 3f;
    public Transform player;
    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    void Update()
    {
        if (GameController.instance.isGamePaused) return;


        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {          
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("PickableObject"))
            {
                if (Vector3.Distance(player.transform.position, hit.transform.position) <= 3f)
                {
                    pickableObjectOnTheFloor = hit.collider.gameObject.GetComponent<PickableObject>();
                    if (pickableObjectOnTheFloor)
                    {
                        pickableObjectOnTheFloor.displayInfo();
                        if (Input.GetMouseButtonDown(1))
                        {
                            pickableObjectOnTheFloor.onPickup();
                            pickableObjectOnTheFloor = null;
                        }
                    }
                }
            }
            else
            {
                if (pickableObjectOnTheFloor)
                {
                    pickableObjectOnTheFloor.hideInfo();
                }
            }
        }
        else
        {
            if (pickableObjectOnTheFloor)
            {
                pickableObjectOnTheFloor.hideInfo();
            }
            else
            {
                pickableObjectOnTheFloor = null;
            }
        }

    }

}

