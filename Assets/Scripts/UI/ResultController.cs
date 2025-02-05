using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Prototype.AudioCore;
using UnityEngine;
using UnityEngine.UI;

public class ResultController : MonoBehaviour
{
   public static event Action OnCreateParticles;
   
   public GameObject panelResult;
   
   public GameObject panelFail;

   public GameObject panelInGame;
   
   public UIRewardController UIReward;

   public GameObject slider;
   
   public GameObject setings;

   public Sprite bestSprite;

   public Sprite recordSprite;

   public GameObject best;

   public GameObject record;
   
   public Text distanceText;

   public GameObject success;
   
   public GameObject finishLine;

   private bool isStarted;
   private void OnEnable()
   {
      PlayerController.OnShowResult += Show;
      
      PlayerController.OnShowFail += Fail;
   }

   private void OnDisable()
   {
      PlayerController.OnShowResult -= Show;
      
      PlayerController.OnShowFail -= Fail;
   }

   private void Start()
   {
      //panelInGame.SetActive(true);
   }

   private void Fail(float delay)
   {
      DOVirtual.DelayedCall(delay, () =>
      {
         panelFail.SetActive(true);

         AudioController.PlaySound("endGame");
         
         slider.SetActive(false);
         
         finishLine.SetActive(false);
      });
   }
   
   private void Show(float delay, int distance)
   {
      DOVirtual.DelayedCall(delay, () =>
      {
         panelResult.SetActive(true);

         AudioController.Release(true);
         
         AudioController.PlaySound("endGame");
         
         //panelInGame.SetActive(false);
         Debug.Log("!!; "+ PlayerController.isFailed);
         //success.SetActive(!PlayerController.isFailed);
         
         slider.SetActive(false);
         
         finishLine.SetActive(false);
         
         UIReward.Change(distance);

         best.SetActive(PlayerPrefs.GetInt("IsRecord", 1) != 1);
         
         record.SetActive(PlayerPrefs.GetInt("IsRecord", 1) == 1);
         
         distanceText.text = PlayerPrefs.GetInt("BestDistance", 0).ToString();

         OnCreateParticles?.Invoke();
      });
   }
   
   private void Hide()
   {
      panelResult.SetActive(false);
   }
   
   public void BttnSettings()
   {
      if(setings.activeSelf)
         return;
      
      setings.SetActive(true);

      isStarted = Time.timeScale == 1;
      
      Time.timeScale = 0;
   }
   
   public void Continue()
   {
      if(isStarted)
         Time.timeScale = 1;
      
      setings.SetActive(false);
   }
}
