using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    public List<ParticleSystem> particles;
    
    private void OnEnable()
    {
        ResultController.OnCreateParticles += Play;
    }

    private void OnDisable()
    {
        ResultController.OnCreateParticles -= Play;
    }

    private void Play()
    {
        {
            foreach (var particle in particles)
            {
                particle.Play();
            }
        }
    }
}
