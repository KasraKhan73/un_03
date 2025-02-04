using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPivot : MonoBehaviour
{
   public Transform plane;

   public float backend;

   public float frontend;

   public void Update()
   {
      if (Input.GetKeyDown(KeyCode.F1))
      {
         plane.localPosition += new Vector3(0,0, backend);
      }
      
      if (Input.GetKeyDown(KeyCode.F2))
      {
         plane.localPosition += new Vector3(0,0, frontend);
      }
   }
}
