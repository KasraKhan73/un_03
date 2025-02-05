using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using Lunha;
using UnityEngine;

public class DropElements : MonoBehaviour
{
   [Header("Найменування")]
   public PLaneParts name;

   [Header("Колір")]
   public PlaneColors color;
   
   [Header("Дочірні об*єети")]
   public List<Rigidbody> rigidbodies;
   
   [Header("Точка імпульса")]
   public GameObject COM;
    
   [Header("Сила імпульса")]
   public float ForcePower = 30;
   
   private List<Transform> _child = new List<Transform>();
   
   private Tween _moveTween;
   
   private float _moveSpeed = 1;

   private Animator _animator;

   private void Awake()
   {
      _animator = GetComponent<Animator>();
   }

   private void Start()
   {
      foreach (var r in rigidbodies)
      {
         _child.Add(r.transform);
      }
   }

   public void Init(PlaneColors newColor)
   {
      color = newColor;
      
      foreach (var r in rigidbodies)
      {
         r.GetComponent<MaterialController>()?.SetMaterial(color);
      }
   }

   public void PlayDropAnimation()
   {
      _animator.Play("Drop");
   }

   public void Drop()
   {
      transform.SetParent(null);

      foreach (var r in rigidbodies)
      {
         r.isKinematic = false;

         r.useGravity = true;
      }

      //foreach (var child in _child)
      //{
      //   var direction = child.position - COM.transform.position;
      //   child.GetComponent<Rigidbody>().AddForce(direction * ForcePower);
      //}

      Destroy(gameObject, 2);
   }

   public void DropWithoutAnimation()
   {
         transform.SetParent(null);

         foreach (var r in rigidbodies)
         {
            r.isKinematic = false;
         
            r.useGravity = true;
         }

         foreach (var child in _child)
         {
            var direction = child.position - COM.transform.position;
            child.GetComponent<Rigidbody>().AddForce(direction * ForcePower);
         }

         Destroy(gameObject, 2);
   }

   public void PlayAnimation(BazierCurveCubic _moveCurve)
   {
      var targetPoints = new Vector3[100];

      for ( var i = 0; i < 100; i++ )
      {
         targetPoints[i] = BazierCurves.GetCubicPosition ( ref _moveCurve.curves, ( ( float ) i ) / 100 );
      }

      _moveTween = transform.DOLocalPath ( targetPoints, _moveSpeed )
         .SetEase ( Ease.Linear )
         .SetSpeedBased ( false )
         .SetLookAt ( 0.01f, Vector3.forward )
         .OnStart(()=> transform.DOLocalRotate(Vector3.zero, _moveSpeed, RotateMode.FastBeyond360));
   }
}
