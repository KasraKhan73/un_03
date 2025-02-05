using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Zone : MonoBehaviour
{
    public static event Action<int> _OnZoneEntered;

    [SerializeField] int comboMod;

    private void OnTriggerEnter(Collider plane)
    {
        _OnZoneEntered?.Invoke(comboMod);
    }
}
