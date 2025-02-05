using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIRewardController : MonoBehaviour
{
   [Header("Поле")]
   public Text field;
   
   [Header("Префаб для монеток, що літають")]
   public GameObject flyCoin;

   [Header("Стартова позиція")]
   public Transform startpos;

   [Header("Кофіцієнт")] 
   public int kof = 10;
   
   private int score = 0;

   public void Change(int newValue)
   {
      DOVirtual.Float(0,  newValue / kof, 1f, f =>
         {
            score = (int)f;
            field.text = score.ToString();
         })
         .OnStart(Started)
         .OnComplete(Completed)
         .SetDelay(0.5f);
   }

   private void Completed()
   {
      StartCoroutine(CreateFlyScore());
      
      return;
      
      var go = Instantiate(flyCoin, startpos.position, Quaternion.identity);

      go.GetComponent<FlyScoreController>().score = score;

      DOVirtual.DelayedCall(0, () =>
      {
         go = Instantiate(flyCoin, startpos.position, Quaternion.identity);

         go.GetComponent<FlyScoreController>().score = 0;
      }).SetDelay(0.2f);
   }

   private IEnumerator CreateFlyScore()
   { 
      var go = Instantiate(flyCoin, startpos.position, Quaternion.identity);
      go.GetComponent<FlyScoreController>().score = score;

      yield return new WaitForSeconds(0.1f);
      
      go = Instantiate(flyCoin, startpos.position, Quaternion.identity);
      go.GetComponent<FlyScoreController>().score = 0;
      
      yield return new WaitForSeconds(0.1f);
      
      go = Instantiate(flyCoin, startpos.position, Quaternion.identity);
      go.GetComponent<FlyScoreController>().score = 0;
   }
   
   private void Started()
   {
       
   }
}
