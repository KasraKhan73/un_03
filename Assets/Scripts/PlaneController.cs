using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float speed = 15;
    
    private Rigidbody _rigidbody;

    private void OnEnable()
    {
        //DragObject.OnImpulse += Impulse;
        
        //SlingshotController.OnImpulse += Impulse;
    }

    private void OnDisable()
    {
        //DragObject.OnImpulse -= Impulse;
        
        //SlingshotController.OnImpulse -= Impulse;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    private void Start()
    {
        
    }

    private void Impulse(float strength)
    {
        Debug.Log("Impulse");
        
        _rigidbody.AddForce(transform.forward * strength, ForceMode.Impulse);

        _rigidbody.useGravity = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Impulse(11);
        }
    }
}
