using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneController : MonoBehaviour
{
   private Transform _transform;

   private MeshRenderer _meshRenderer;
   
   private void OnEnable()
   {
      PlayerController.OnFlying += Show;
   }

   private void OnDisable()
   {
      PlayerController.OnFlying -= Show;
   }

   private void Awake()
   {
      _transform = GetComponent<Transform>();
      
      _meshRenderer = GetComponent<MeshRenderer>();

      _meshRenderer.enabled = false;
      
      var scale = PlayerPrefs.GetInt("BestDistance", 1);
      _transform.localScale = new Vector3(scale * 2, scale * 2, scale * 2);
   }

   private void Show()
   {
      _meshRenderer.enabled = true;
   }
}
