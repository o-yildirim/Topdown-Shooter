using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : MonoBehaviour
{
    public int collisionRequiredToIgnite = 5;
    public float firePropagationRate = 0.5f;
    public float circleCastRadius = 2f;

    public LayerMask fireMask;


    private List<ParticleCollisionEvent> collisionEvent = new List<ParticleCollisionEvent>();

    private GameObject fullyBurningFireEffect;


    public float reduceParticleRatio = 2f;
    public float destroyChildSeconds = 0.25f;

    public enum BurningStates {NotBurning,FullyBurning,Optimizing,Optimized };
    private BurningStates states;
    
    void Start()
    {
        states = BurningStates.NotBurning;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (states != BurningStates.NotBurning) return;

        collisionRequiredToIgnite--;
        if (collisionRequiredToIgnite <= 0)
        {
            Enemy enemy = GetComponent<Enemy>();
            if (enemy)
            {
                enemy.die(enemy.transform.position); //Enemy will be in burning state
            }
            else
            {
                Flamethrower flamethrower = other.transform.parent.GetComponent<Flamethrower>();
                flamethrower.ps.GetCollisionEvents(gameObject, collisionEvent);

                if (Physics2D.OverlapCircle(collisionEvent[0].intersection, circleCastRadius, fireMask))
                {
                    return;
                }

                GameObject fireEffect = flamethrower.fireEffectPrefab;
                StartCoroutine(burn(fireEffect, collisionEvent[0].intersection));
            }

        }

    }



    public IEnumerator burn(GameObject fireEffect, Vector3 position)
    {
        if(states == BurningStates.FullyBurning) yield break;

        position.z = 0f;
        GameObject createdFirePrefab = Instantiate(fireEffect, position, fireEffect.transform.rotation);
        createdFirePrefab.transform.parent = transform;
        createdFirePrefab.transform.localScale = fireEffect.transform.localScale;

        GameObject createdFireEffect = createdFirePrefab.transform.GetChild(0).gameObject;

        ParticleSystem ps = createdFireEffect.GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule shape = ps.shape;
        ParticleSystem.EmissionModule emission = ps.emission;
        float defaultEmission = emission.rateOverTime.constant;
        Vector2 defaultFireScale = shape.scale;


       // Debug.Log("SA");

        bool fullyPropagated = false;
        bool fullyPositioned = false;
        while (true)
        {

            Vector3 newParticleScale = Vector3.MoveTowards(ps.shape.scale, transform.localScale, firePropagationRate * Time.deltaTime);
            shape.scale = newParticleScale;
            if (shape.scale.x * shape.scale.y >= transform.localScale.x * transform.localScale.y)
            {
                fullyPropagated = true;
            }


            Vector3 newPos = Vector3.MoveTowards(createdFireEffect.transform.position, transform.position, firePropagationRate / 1.5f * Time.deltaTime);
            createdFireEffect.transform.position = newPos;
            if (Vector3.Distance(createdFireEffect.transform.position, transform.position) <= 0.001f)
            {
                fullyPositioned = true;
            }

            float newEmissionRate = (shape.scale.x * shape.scale.y * defaultEmission) / (defaultFireScale.x * defaultFireScale.y);
            emission.rateOverTime = newEmissionRate;

            ps.maxParticles = (int)(emission.rateOverTime.constant * ps.main.duration) + 10;

            if (fullyPositioned && fullyPropagated)
            {
                fullyBurningFireEffect = createdFirePrefab;
                states = BurningStates.FullyBurning;
                break;
            }

            yield return null;


        }
        StopAllCoroutines();
        StartCoroutine(optimizeBurning());
    }

    public IEnumerator optimizeBurning()
    {
        if (states != BurningStates.FullyBurning) yield break;

        states = BurningStates.Optimizing;

        Debug.Log("Optimizasyon top isidir");

        ParticleSystem fullyBurningParticle = fullyBurningFireEffect.GetComponentInChildren<ParticleSystem>();

        float oneFullyBurningFireEmission = fullyBurningParticle.emission.rateOverTime.constant;

        //DENEME BURADAN AŞAĞISI DİĞER COMMENTE KADAR
        float[] particleEmissions = new float[transform.childCount];



        for (int i = transform.childCount - 1; i >= 0; i--)
        {

            GameObject currentChild = transform.GetChild(i).gameObject;
            if (currentChild != fullyBurningFireEffect)
            {
                float emissionOfCurrentChild = currentChild.GetComponentInChildren<ParticleSystem>().emission.rateOverTime.constant;
                particleEmissions[i] = emissionOfCurrentChild;

                fullyBurningParticle.maxParticles += (int)(emissionOfCurrentChild * fullyBurningParticle.duration);
                fullyBurningParticle.emissionRate += emissionOfCurrentChild;
                Destroy(currentChild);
            }

            yield return new WaitForSeconds(destroyChildSeconds);
        }


        Debug.Log(oneFullyBurningFireEmission);

        for(int i = 0; i < particleEmissions.Length; i++)
        {
            //fullyBurningParticle.emissionRate -= particleReductionPerIteration;
            //fullyBurningParticle.maxParticles -= (int)(particleReductionPerIteration * fullyBurningParticle.duration);

            fullyBurningParticle.emissionRate -= particleEmissions[i];
            fullyBurningParticle.maxParticles -= (int)(particleEmissions[i] * fullyBurningParticle.duration);

            yield return new WaitForSeconds(reduceParticleRatio);
        }

        fullyBurningParticle.emissionRate = oneFullyBurningFireEmission;

        states = BurningStates.Optimized;
        //





        /* AŞAĞISI ORJINAL
        for (int i = transform.childCount-1; i >= 0; i--)
        {

            GameObject currentChild = transform.GetChild(i).gameObject;
            if (currentChild != fullyBurningFireEffect)
            {
                float emissionOfCurrentChild = currentChild.GetComponentInChildren<ParticleSystem>().emission.rateOverTime.constant;
                fullyBurningParticle.maxParticles += (int) (emissionOfCurrentChild * fullyBurningParticle.duration);
                fullyBurningParticle.emissionRate += emissionOfCurrentChild;              
                Destroy(currentChild);
            }

            yield return new WaitForSeconds(destroyChildSeconds);
        }


        Debug.Log(oneFullyBurningFireEmission);

        while (fullyBurningParticle.emissionRate > oneFullyBurningFireEmission)
        {
            //fullyBurningParticle.emissionRate -= particleReductionPerIteration;
            //fullyBurningParticle.maxParticles -= (int)(particleReductionPerIteration * fullyBurningParticle.duration);

            fullyBurningParticle.emissionRate -= oneFullyBurningFireEmission;
            fullyBurningParticle.maxParticles -= (int)(oneFullyBurningFireEmission * fullyBurningParticle.duration);

            yield return new WaitForSeconds(reduceParticleRatio);
        }

        fullyBurningParticle.emissionRate = oneFullyBurningFireEmission;

        states = BurningStates.Optimized;
        */
    }

}
