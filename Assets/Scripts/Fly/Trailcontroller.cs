using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trailcontroller : MonoBehaviour
{
    private TrailRenderer _trailRenderer;
    
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
        _trailRenderer = GetComponent<TrailRenderer>();
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
        _trailRenderer.enabled = true;
    }
    
    private void Disable()
    {
        _trailRenderer.enabled = false;
    }
}
