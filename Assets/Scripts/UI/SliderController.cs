using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
   private Slider _slider;

   private void Awake()
   {
      _slider = GetComponent<Slider>();
   }

   private void Start()
   {
      Change(0.35f);
   }

   private void Change(float newValue)
   {
      _slider.DOValue(newValue, 1.0f).SetDelay(0.5f);
   }
}
