using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using Prototype.AudioCore;

public class Trash : MonoBehaviour
{
    public static event Action<PLaneParts, PlaneColors> _OnTrashCollected;

    [Header("Шестирьонка замість деталей")]
    public bool isOneDesign;
    
    [Header("Колір літака")]
    public PlaneColors color;
    
    [Header("Найменування деталі")]
    public PLaneParts name;

    [Header("Точка для імпульса")]
    public GameObject COM;

    [Header("Частини літака")]
    public List<GameObject> parts;
    
    [Header("Обгортка, для анімації")]
    public Transform wraper;
    
    private float ForcePower = 30;
    
    public List<Transform> _child = new List<Transform>();

    public GameObject oneDesign;

    public GameObject particle;

    private SpriteRenderer shadow;

    private Collider _collider;

    private MaterialController _materialController;
    
    private void Awake()
    {
        shadow = GetComponentInChildren<SpriteRenderer>();
        
        _materialController = GetComponentInChildren<MaterialController>();

        _collider = GetComponent<Collider>();

        if (shadow)
            shadow.color = name != PLaneParts.None ? Color.green : Color.red;
    }

    private void Start()
    {
        if(color == PlaneColors.None)
            return;

        if (!isOneDesign)
        {
            var go = Instantiate(parts[(int)name - 1], wraper).GetComponentInChildren<TrashPartController>();
        
            go.Init(color);
        }
        else
        {
            oneDesign.SetActive(true);
        }
        
        //_materialController.SetMaterial(color);
    }

    private Road road;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            _collider.enabled = false;
            
            _OnTrashCollected?.Invoke(isOneDesign? PLaneParts.OneDesign : name, color);

            if (name != PLaneParts.None)
            {
                Instantiate(particle, transform.position, Quaternion.identity);
                
                Destroy(this.gameObject);
            }
            else
            {
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                
                if(!AudioController.IsSoundPlaying("Impact"))
                    AudioController.PlaySound("Impact", StreamGroup.FX, 0.5f);
                
                Drop(collider.gameObject.transform);
            }
        }
    }

    public void SetRoadParent(Road road)
    {
        this.road = road;
    }

    private void Drop(Transform player = null)
    {
        shadow.enabled = false;
        
        foreach (var child in _child)
        {
            var direction = child.position - (player? player.position : COM.transform.position);

            if (!child.GetComponent<Rigidbody>().useGravity)
            {
                child.GetComponent<Rigidbody>().useGravity = true;
                child.GetComponent<Rigidbody>().isKinematic = false;
            }

            child.GetComponent<Rigidbody>().AddForce(direction * ForcePower);
        }
        
        Destroy(gameObject,2);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Drop();
        }
    }
}
