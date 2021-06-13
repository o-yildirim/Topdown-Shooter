using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;
    public float decayRate = 8f;
    public float pauseTime = 5f;
    private float currentTime = 0f;
    void Start()
    {
        InitializeIfNeeded();
    }

    private void Update()
    {
        if (ps.isPaused) return;

        currentTime += Time.deltaTime;
        if(currentTime >= pauseTime)
        {
            ps.Pause();
        }
        // GetParticles is allocation free because we reuse the m_Particles buffer between updates
        int numParticlesAlive = ps.GetParticles(particles);

        // Change only the particles that are alive
        for (int i = 0; i < numParticlesAlive; i++)
        {
            if (particles[i].velocity.magnitude >= 0f)
                particles[i].velocity -= particles[i].velocity.normalized / decayRate;
        }

        // Apply the particle changes to the Particle System
        ps.SetParticles(particles, numParticlesAlive);
    }

    void InitializeIfNeeded()
    {
        if (ps == null)
            ps = GetComponent<ParticleSystem>();

        if (particles == null || particles.Length < ps.main.maxParticles)
            particles = new ParticleSystem.Particle[ps.main.maxParticles];
    }

}
