using System;
using System.Collections;
using System.Collections.Generic;
using Prototype.SceneLoaderCore.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
   [SerializeField] private int level;

   private Button _button;

   private void Awake()
   {
      _button = GetComponent<Button>();
      
      _button.onClick.AddListener(Click);
   }

   private void Click()
   {
      PlayerPrefs.SetInt("Level", level);
      PlayerPrefs.Save();

      if (SceneLoader.Instance)
      {
         SceneLoader.Instance.SwitchToScene("RunScene");
      }
      else
      {
         SceneManager.LoadScene("RunScene");
      }
   }
}
