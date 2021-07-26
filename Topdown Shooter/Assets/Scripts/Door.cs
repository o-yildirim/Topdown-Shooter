using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Door : MouseInteractableObject
{
    public GameObject infoCanvas;
    public Animator doorAnimator;
    public int doorId;
    private bool isOpen = false;
    private Transform player;
   

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        checkDatabase();
    }


    public override void onRightClick()
    {
        if (isOpen) return;

        Vector3 direction = player.position - transform.position;
        float angle = Vector3.Angle(direction, transform.up);

   

        if (Mathf.Abs(angle) < 90f)
        {
            openDoor();
         
        }
        else
        {
            //Locked from the other side
        }
    }

    public override void displayInfo()
    {
        if (infoCanvas)
        {
            infoCanvas.SetActive(true);
        }
    }

    public override void hideInfo()
    {
        if (infoCanvas)
        {
            infoCanvas.SetActive(false);
        }
    }

    public void openDoor()
    {
        isOpen = true;
        doorAnimator.SetTrigger("openDoor");
        Destroy(infoCanvas);


        float animationDuration = doorAnimator.GetCurrentAnimatorStateInfo(0).length;
        StartCoroutine(astarScan(animationDuration));

        string doorKey = "door" + doorId;
        PlayerPrefs.SetInt(doorKey, 1);

        
    }

    public void checkDatabase()
    {
        string doorKey = "door" + doorId;
        if (PlayerPrefs.HasKey(doorKey)) 
        {
            int doorOpen = PlayerPrefs.GetInt(doorKey);
            if (doorOpen == 1)
            {
                Destroy(transform.parent.GetComponent<Animator>());
                transform.parent.rotation = Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 90f);           
                isOpen = true;
                Destroy(infoCanvas);
                StartCoroutine(astarScan(0.1f));
            } 
        }
        else
        {
            PlayerPrefs.SetInt(doorKey, 0);
        }
    }

    public IEnumerator astarScan(float waitDuration)
    {
        yield return new WaitForSeconds(waitDuration);
        AstarPath.active.Scan();
    }
}
