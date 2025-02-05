using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlameController : MonoBehaviour
{
    private List<ParticleSystem> _particles;

    private bool isVisible;
    
    private bool isFly;
    
    private void OnEnable()
    {
        PlayerController.OnDropedPlane += Disable;

        PlayerController.OnFlying += Fly;
    }

    private void OnDisable()
    {
        PlayerController.OnDropedPlane -= Disable;
        
        PlayerController.OnFlying -= Fly;
    }

    private void Awake()
    {
        _particles = GetComponentsInChildren<ParticleSystem>().ToList();
    }

    private void Fly()
    {
        isFly = true;
    }

    private void FixedUpdate()
    {
        if(!isFly)
            return;
        
        if (MovementController.IsTap)
        {
            if (!isVisible)
            {
                isVisible = true;

                Enable();
            }
        }
        else
        {
            if (isVisible)
            {
                isVisible = false;

                Disable();
            }
        }
    }

    private void Enable()
    {
        foreach (var particle in _particles)
        {
            particle.Play();
        }
    }
    
    private void Disable()
    {
        foreach (var particle in _particles)
        {
            particle.Stop();
        }
    }
}
