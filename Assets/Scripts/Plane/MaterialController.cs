using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
   public static Action OnDropedPlane;
   
   public List<Material> materials;
   
   private MeshRenderer _meshRenderer;
   
   private void Awake()
   {
      _meshRenderer = GetComponent<MeshRenderer>();
   }

   public void SetMaterial(PlaneColors color)
   {
      if(materials.Count > 0)
         _meshRenderer.material = materials[(int)color - 1];
   }
   
   private void OnTriggerEnter(Collider collider)
   {
      if (collider.CompareTag("Earth"))
      {
         OnDropedPlane?.Invoke();
      }
   }
}
