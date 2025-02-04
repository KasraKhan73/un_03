using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform targetObject;
    
    public Vector3 cameraOffset;

    public float smoothFactor = 0.5f;
    
    public bool lookAtTarget = false;
    
    private void Start()
    {
        cameraOffset = transform.position - targetObject.transform.position;
    }

    private void LateUpdate()
    {
        var newPosition=targetObject.transform.position+cameraOffset;
        
        transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);
        
        if (lookAtTarget) 
        {
            transform.LookAt(targetObject);
        }
    }
}
