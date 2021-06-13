using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Gun
{
    private ParticleSystem ps;
    void Start()
    {
        ps = transform.GetComponentInChildren<ParticleSystem>();
    }


    public override void fire()
    {
        if(!ps.isPlaying) ps.Play();   
    }

    public override void stopFire()
    {
        ps.Stop();
    }
}
