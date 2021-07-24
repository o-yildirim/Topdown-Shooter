using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyStates currentState;
    private Rigidbody2D enemyRb;
    private CombatController combatController;
    private SpriteRenderer spRenderer;
    private CircleCollider2D enemyCollider;
    public GameObject lowerBody;
    public Sprite facedownSprite;
    public Sprite lyingOnBackSprite;
    

    void Start()
    {
        currentState = GetComponent<EnemyStates>();
        enemyRb = GetComponent<Rigidbody2D>();
        spRenderer = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<CircleCollider2D>();

        combatController = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<CombatController>();
        if (combatController)
        {
            combatController.increaseEnemy();
        }
    }

    public void die(Vector3 hitPoint)
    {
        enemyCollider.enabled = false;

        Gun activeGun = GetComponent<EnemyCombat>().getGun();

        GunManagement.instance.dropGun(activeGun,transform.position);
        

        var scripts = GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour script in scripts)
        {
            if(script != this)
            {
                script.enabled = false;
            }
        }
    


        Vector3 direction = hitPoint - transform.position;
        float angle = Vector3.Angle(direction, transform.up) -45f;
 
        Debug.Log("Vector angle: " + angle);

        if (Mathf.Abs(angle)  <= 90f)
        {
            spRenderer.sprite = lyingOnBackSprite;
        }
        else
        {
            spRenderer.sprite = facedownSprite;
        }
        lowerBody.SetActive(false);
        enemyRb.velocity = Vector2.zero;
        currentState.state = EnemyStates.EnemyState.Dead;

        combatController.decreaseEnemy();


        Transform parent = transform.root;
        transform.parent = null;
        Destroy(parent.gameObject); //Şimdilik kalsın buraya sprite falan girerse
       
     
    }

   
    public void becomeWounded()
    {
        //Başka fonksiyonlar gelebilir
        currentState.state = EnemyStates.EnemyState.Unconsious;
    }
   
}
