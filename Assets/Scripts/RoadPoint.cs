using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public enum TRoad
{
    Road,
    Final
}

public class RoadPoint : MonoBehaviour
{
    public static event Action<GameObject> _OnPlayerEntered;
    
    public static event Action<TRoad> _OnCheckRoad;
    
    public TRoad type;

    [SerializeField] private GameObject parent;

    private void OnTriggerEnter(Collider colider)
    {
        if (colider.CompareTag("Player"))
        {
            _OnPlayerEntered?.Invoke(parent);
            
            _OnCheckRoad?.Invoke(type);
        }
    }
}
