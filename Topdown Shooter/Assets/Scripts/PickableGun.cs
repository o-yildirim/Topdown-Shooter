using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableGun : MonoBehaviour
{
    public int gunId;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GunManagement.instance.switchGun(gunId);
            Destroy(gameObject);
        }
    }


}
