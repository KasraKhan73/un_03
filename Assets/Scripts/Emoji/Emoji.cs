using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emoji : MonoBehaviour
{
   public Camera _camera;
   
   public bool useStatic = true;

   private void Start()
   {
      _camera = Camera.main;
   }

   private void LateUpdate()
   {
      if (!useStatic)
      {
         transform.LookAt(_camera.transform);
      }
      else
      {
         transform.rotation = _camera.transform.rotation;
      }

      transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
   }
}
