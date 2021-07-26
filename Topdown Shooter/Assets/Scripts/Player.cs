using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Sprite lyingOnBackSprite;
    public Sprite facedownSprite;

    private SpriteRenderer spRenderer;

    private void Start()
    {
        spRenderer = GetComponent<SpriteRenderer>();
    }

    public void die(Vector3 hitPoint)
    {
        Vector3 direction = hitPoint - transform.position;
        float angle = Vector3.Angle(direction, transform.up) - 45f;

        Debug.Log("Vector angle: " + angle);

        if (Mathf.Abs(angle) <= 90f)
        {
            spRenderer.sprite = lyingOnBackSprite;
        }
        else
        {
            spRenderer.sprite = facedownSprite;
        }
        GameController.instance.displayDeathScreen();
    }
}
