using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIProgresBarController : MonoBehaviour
{
   public float FillSpeed = 0.5f;
   
   private Slider _slider;

   private Tween twn;
   
   private void OnEnable()
   {
      
   }

   private void OnDisable()
   {
      
   }

   private void Awake()
   {
      _slider = GetComponent<Slider>();
   }

   private void Update()
   {
      if (_slider.value < PlayerController.distanceOfFly)
      {
         _slider.value += FillSpeed * Time.deltaTime;
      }
   }

   private void Change(int value = -1)
   {
      twn?.Kill();
      
      twn =  _slider.DOValue(value, 1.0f).SetDelay(0.5f);
   }
}
