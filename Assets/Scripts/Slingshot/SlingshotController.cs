using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SlingshotController : MonoBehaviour
{
    public static event Action OnImpulse;
    
    public static event Action<float> OnClownshot;
    
    public static event Action OnClownBackMove;
    
    public static SlingshotController Instance;
    
    public Transform ObjectHolder;
    
    public Transform leftAnchor;
    
    public Transform rightAnchor;
    
    public LineRenderer[] lines;

    [Header("Сила для 1 елемента")]
    public float strength = 10;

    private Transform _plane;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start () 
    {
        lines [0].SetPosition (0, leftAnchor.position);
        lines [0].gameObject.SetActive(true);
        lines [1].SetPosition (0, rightAnchor.position);
        lines [1].gameObject.SetActive(true);
    }

    public void Init(Transform plane)
    {
        _plane = plane;
        
        _plane.SetParent(ObjectHolder);

        DOVirtual.DelayedCall(0, () =>
        {
            Shot();
        });
    }

    private void Shot()
    {
        var z = ObjectHolder.position.z;
            
        OnClownBackMove?.Invoke();
        
        ObjectHolder.DOMoveZ(z - 4, 1).OnComplete(() =>
        {
            OnClownshot?.Invoke(0);
            
            ObjectHolder.DOMoveZ(z, 0.1f).SetDelay(0.5f).OnComplete(() =>
            {
                if(_plane)
                    _plane.SetParent(null);
                
                OnImpulse?.Invoke();
            });
        });
    }

    private void Update ()
    {
        foreach (var t in lines)
        {
            t.SetPosition (1, ObjectHolder.position);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Shot();
        }
    }
}
