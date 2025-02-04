using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToPlay : MonoBehaviour
{
   public GameObject panel;
   
   private void Awake()
   {
      Time.timeScale = 0;
   }

   public void Tap()
   {
      Time.timeScale = 1;
      
      panel.SetActive(false);
   }
}
