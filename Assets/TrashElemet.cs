using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using Prototype.AudioCore;
using UnityEngine;

public class TrashElemet : MonoBehaviour
{
    public static event Action<int> OnTrashCollected;
    
    public GameObject parent;
    
    public GameObject particle;

    private int ParentID;
    
    private Collider _collider;

    private Rigidbody _rigidbody;
    
    private void Awake()
    {
        _collider = GetComponent<Collider>();
        
        _rigidbody = GetComponent<Rigidbody>();

        ParentID = parent.GetInstanceID();
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            _collider.enabled = false;
            
            OnTrashCollected?.Invoke(ParentID);

            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                
            if(!AudioController.IsSoundPlaying("Impact"))
                AudioController.PlaySound("Impact", StreamGroup.FX, 0.5f);
                
            Drop(collider.gameObject.transform);
            
            if(particle)
                Instantiate(particle, transform.position, Quaternion.identity);
        }
    }

    private void Drop(Transform player = null)
    {
        if (player != null)
        {
            var direction = transform.position - new Vector3(player.position.x, transform.position.y - 1, player.position.z);
        
            if (!_rigidbody.useGravity)
            {
                _rigidbody.useGravity = true;
                _rigidbody.isKinematic = false;
            }

            _rigidbody.AddForce(direction * 2);
        }

        Destroy(gameObject,2);
    }
}
