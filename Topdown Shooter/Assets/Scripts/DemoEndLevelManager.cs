using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoEndLevelManager : MonoBehaviour, LevelController
{
    public ElevatorDoor elevator;
    public Transform spawnPoint;






    public void onLevelLoad()
    {
        Player.instance.transform.position = spawnPoint.position;
        Player.instance.transform.up = Vector3.down;
    }
    public void endLevel() { }
    public IEnumerator startLevel()
    {
        yield return null;
    }
}
