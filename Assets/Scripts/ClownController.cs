using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ClownController : MonoBehaviour
{
    private Animator _animator;

    private void OnEnable()
    {
        SlingshotController.OnClownshot += SetParent;
        
        SlingshotController.OnClownBackMove += MoveBack;

        PlayerController.OnClownshot += SetParent;
    }

    private void OnDisable()
    {
        SlingshotController.OnClownshot -= SetParent;
        
        SlingshotController.OnClownBackMove -= MoveBack;
        
        PlayerController.OnClownshot -= SetParent;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void SetParent(float delay)
    {
        transform.SetParent(null);

        DOVirtual.DelayedCall(2, () =>
        {
            Destroy(gameObject);
        });
    }

    private void MoveBack()
    {
        _animator.SetBool("BackMove", true);
    }
}
