using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    private LevelController levelManager;
    void Start()
    {
        GameObject levelManagerObject = GameObject.FindGameObjectWithTag("LevelManager");
        if (levelManagerObject)
        {
            levelManager = levelManagerObject.GetComponent<LevelController>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelManager != null)
        {
            levelManager.endLevel();
        }
    }
}
