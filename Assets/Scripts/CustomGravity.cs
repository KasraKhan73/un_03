using System;
using UnityEngine;
 
[RequireComponent(typeof(Rigidbody))]
public class CustomGravity : MonoBehaviour
{
    public float gravityScale = 1.0f;

    private static float globalGravity = -9.81f;

    private Rigidbody m_rb;

    private void OnEnable ()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.useGravity = false;
    }

    private void Start()
    {
        var gravity = globalGravity * gravityScale * Vector3.up;
        //m_rb.AddForce(gravity, ForceMode.Acceleration);
    }

    private void FixedUpdate ()
    {
        var gravity = globalGravity * gravityScale * Vector3.up;
        //m_rb.AddForce(gravity, ForceMode.Acceleration);
    }
}