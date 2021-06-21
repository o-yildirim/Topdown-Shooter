using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : MonoBehaviour
{
    public int collisionRequiredToIgnite = 5;
    public float firePropagationRate = 0.5f;

    private bool isBurning = false;
    private List<ParticleCollisionEvent> collisionEvent = new List<ParticleCollisionEvent>();
    private void OnParticleCollision(GameObject other)
    {
        /*
            Flamethrower flamethrower = other.transform.parent.GetComponent<Flamethrower>();
            GameObject fireEffect = flamethrower.fireEffectPrefab;
            flamethrower.ps.GetCollisionEvents(gameObject,collisionEvent);

            Instantiate(fireEffect, collisionEvent[0].intersection, fireEffect.transform.rotation);
            */
        // Debug.Log(collisionEvent.intersection);

        collisionRequiredToIgnite--;
        if (collisionRequiredToIgnite <= 0)
        {
            Enemy enemy = GetComponent<Enemy>();
            if (enemy)
            {
                enemy.die();
            }
            else
            {
                if (!isBurning)
                {
                    isBurning = true;
                    Flamethrower flamethrower = other.transform.parent.GetComponent<Flamethrower>();
                    GameObject fireEffect = flamethrower.fireEffectPrefab;
                    flamethrower.ps.GetCollisionEvents(gameObject, collisionEvent);
                    StartCoroutine(burn(fireEffect, collisionEvent[0].intersection));
                }

            }

        }

    }

    public IEnumerator burn(GameObject fireEffect, Vector3 position)
    {


        GameObject createdFireEffect = Instantiate(fireEffect, position, fireEffect.transform.rotation);
        ParticleSystem ps = createdFireEffect.GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule shape = ps.shape;
        ParticleSystem.EmissionModule emission = ps.emission;
        float defaultEmission = emission.rateOverTime.constant;
        Vector2 defaultFireScale = shape.scale;



        bool fullyPropagated = false;
        bool fullyPositioned = false;
        while (true)
        {
           
            Vector3 newParticleScale = Vector3.MoveTowards(ps.shape.scale, transform.localScale, firePropagationRate * Time.deltaTime);
            shape.scale = newParticleScale;
            if(shape.scale.x * shape.scale.y >= transform.localScale.x * transform.localScale.y)
            {
                fullyPropagated = true;
            }

            Vector3 newPos = Vector3.MoveTowards(createdFireEffect.transform.position, transform.position, firePropagationRate/1.5f * Time.deltaTime);
            createdFireEffect.transform.position = newPos;
            if(Vector3.Distance(createdFireEffect.transform.position,transform.position) <= 0.001f)
            {
                fullyPositioned = true;
            }



            float newEmissionRate = (shape.scale.x * shape.scale.y * defaultEmission) /(defaultFireScale.x * defaultFireScale.y)  ;
            emission.rateOverTime = newEmissionRate;

            ps.maxParticles = (int) ( emission.rateOverTime.constant *  ps.main.duration) + 10;

            if (fullyPositioned && fullyPropagated) break;


            yield return null;
        }      
    }


}
