using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MultiplierController : MonoBehaviour
{
    public static event Action<float> OnShowResult;
    
    public static event Action<TMultiplier> _OnZoneEntered;
    
    [Header("Мнижник")]
    public TMultiplier multiplier;

    [Header("Партікли")]
    public GameObject particlesPrefab;

    public void OnEnable()
    {
        PlayerController.OnFinishFlaying += CreateParticles;
    }

    public void OnDisable()
    {
        PlayerController.OnFinishFlaying -= CreateParticles;
    }

    private void CreateParticles(TMultiplier _multiplier)
    {
        if(_multiplier != multiplier) return;
        
        Instantiate(particlesPrefab, this.gameObject.transform.position, Quaternion.identity, null);
    }
    
    private void OnTriggerEnter(Collider plane)
    {
        if (plane.CompareTag("Player"))
        {
            //CreateParticles(multiplier);

            plane.GetComponent<PlayerController>().EndFlying();
        }
    }
}

public enum TMultiplier
{
    X1,
    X2,
    X3,
    X4,
    X5,
    X6,
    X7,
    X8,
    X9,
    X10,
    X11,
    X12
}
