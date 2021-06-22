using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    private PickableGun gunOnTheFloor;
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
            Debug.Log(hit.transform.gameObject.name);
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("PickableGun"))
            {
                if (Vector3.Distance(player.transform.position, hit.transform.position) <= 3f)
                {
                    gunOnTheFloor = hit.collider.gameObject.GetComponent<PickableGun>();
                    if (gunOnTheFloor)
                    {
                        gunOnTheFloor.display();
                        if (Input.GetMouseButtonDown(1))
                        {
                            GunManagement.instance.switchGun(gunOnTheFloor.gunId);
                            Destroy(gunOnTheFloor.transform.root.gameObject);
                            gunOnTheFloor = null;
                        }
                    }
                }
            }
            else
            {
                if (gunOnTheFloor)
                {
                    gunOnTheFloor.hide();
                }
            }
        }
        else
        {
            //Debug.Log("Bosluk");
            if (gunOnTheFloor)
            {
                gunOnTheFloor.hide();
            }
            else
            {
                gunOnTheFloor = null;
            }
        }

    }

}

