using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Gun
{
    private ParticleSystem ps;
    private float nextFire;
    void Start()
    {
        ps = transform.GetComponentInChildren<ParticleSystem>();
    }


    public override void fire()
    {
        if (Time.time < nextFire) return;

        nextFire = Time.time + fireRate;
        ps.Play();   
    }

    public override void stopFire()
    {
        ps.Stop();
    }
}
