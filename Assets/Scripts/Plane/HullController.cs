using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullController : MonoBehaviour
{
    public static Action OnDropedPlane;

    private Collider _collider;
    
    private void OnEnable()
    {
        PlayerController.OnFlying += Fly;
    }

    private void OnDisable()
    {
        PlayerController.OnFlying -= Fly;
    }
    
    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void Fly()
    {
        _collider.enabled = true;
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Earth"))
        {
            OnDropedPlane?.Invoke();
        }
    }
}
