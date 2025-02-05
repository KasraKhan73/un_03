using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreController : MonoBehaviour
{
   public static event Action OnInteractiveBttnContinue; 
   
   private static RectTransform _rect;

   public Text field;

   private int score = 0;
   
   private void OnEnable()
   {
      FlyScoreController.OnChangeScore += Change;
   }

   private void OnDisable()
   {
      FlyScoreController.OnChangeScore -= Change;
   }

   private void Change(int newScore)
   {
      DOVirtual.Float(score, score + newScore, 1f, f =>
         {
            score = (int)f;
            field.text = score.ToString();
         })
         .OnStart(Started)
         .OnComplete(Completed);
   }

   private void Awake()
   {
      _rect = GetComponent<RectTransform>();

      score = PlayerPrefs.GetInt("Coin", 0);
      
      field.text = score.ToString();
   }

   public static Vector2 GetPosition()
   {
      return _rect.position;
   }
   
   private void Completed()
   {
      PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin", 0) + score);
      PlayerPrefs.Save();

      OnInteractiveBttnContinue?.Invoke();
   }

   private void Started()
   {
       
   }
}
