﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Gun
{
    public ParticleSystem ps;
    public GameObject fireEffectPrefab;
    void Start()
    {
        ps = transform.GetComponentInChildren<ParticleSystem>();
    }


    public override void fire()
    {
        if (Time.time < nextFire || bullets <= 0) return;

        nextFire = Time.time + fireRate;
        ps.Play();
        bullets--;
        GunManagement.instance.bulletTextUI.text = bullets.ToString();
    }

    public override void stopFire()
    {
        ps.Stop();
    }

    /*
    private void OnParticleCollision(GameObject other)
    {
   
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

        int numCollisionEvents = ps.GetCollisionEvents(other, collisionEvents);
        Debug.Log(numCollisionEvents);

        int i = 0;
        while (i < numCollisionEvents)
        {

            Instantiate(fireEffectPrefab, collisionEvents[i].intersection, fireEffectPrefab.transform.rotation);
        }

    }
    */
}
