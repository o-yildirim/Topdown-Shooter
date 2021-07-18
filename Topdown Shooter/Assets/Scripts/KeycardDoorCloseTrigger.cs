using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardDoorCloseTrigger : MonoBehaviour
{
    public KeycardReader targetDoorReader;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            targetDoorReader.lockDoor();
        }
    }
}
