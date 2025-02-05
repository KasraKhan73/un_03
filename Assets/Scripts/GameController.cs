using System;
using System.Collections;
using System.Collections.Generic;
using Prototype.AudioCore;
using Prototype.SceneLoaderCore.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
   public bool isRandom = true;
   
   public List<GameObject> levels;

   public List<Transform> points;

   public Transform personage;

   public CameraMovement cameraMovement;
   
   public List<GameObject> earths;

   public int levelTest = 1;
   private void OnEnable()
   {
      PlayerController.OnDropedPlane += ChangeLevel;
   }

   private void OnDisable()
   {
      PlayerController.OnDropedPlane -= ChangeLevel;
   }
   
   private void Awake()
   {
       var index = isRandom? PlayerPrefs.GetInt("Level", 1) : levelTest;
       levels[index - 1].SetActive(true);
       
       earths[(index > 1? 1 : 0)].SetActive(true);

       personage.position = points[index - 1].position;

       cameraMovement.Init();

      if (PlayerPrefs.HasKey("game_start"))
      {
         PlayerPrefs.SetInt("game_start", PlayerPrefs.GetInt("game_start", 0) + 1);
         PlayerPrefs.Save();
      }
      else
      {
         PlayerPrefs.SetInt("game_start", 1);
         PlayerPrefs.Save();
      }
   }

   private void Start()
   {
      AudioController.PlayMusic("music_runner");
   }

   private void ChangeLevel()
   {
      var nextLevel = PlayerPrefs.GetInt("Level", 1) + 1;
      var progress = PlayerPrefs.GetInt("Progress", 1);
      
      PlayerPrefs.SetInt("CompletedLevels", (nextLevel - 1));
      PlayerPrefs.SetInt("UnlockedLevels", nextLevel);
      
      //PlayerPrefs.SetInt("Level", nextLevel > 3? 1 : nextLevel);
      PlayerPrefs.SetInt("Level", nextLevel > levels.Count? 2 : nextLevel);
      PlayerPrefs.SetInt("Progress", progress + 1);
      PlayerPrefs.Save();     
   }

   public void Restart()
   {
      if (SceneLoader.Instance)
      {
         SceneLoader.Instance.SwitchToScene("MainMenu");
      }
      else
      {
         SceneManager.LoadScene("MainMenu");
      }
   }
}
