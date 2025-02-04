using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
   private Text _field;

   private void Awake()
   {
      _field = GetComponent<Text>();
   }

   private void FixedUpdate()
   {
      _field.text = ((int)Time.time).ToString();
   }
}
