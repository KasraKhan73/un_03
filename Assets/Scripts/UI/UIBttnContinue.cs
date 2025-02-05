using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBttnContinue : MonoBehaviour
{
  public GameObject _button;
  
  private void OnEnable()
  {
    UIScoreController.OnInteractiveBttnContinue += OnInteractiveBttnContinue;
  }

  private void OnDisable()
  {
    UIScoreController.OnInteractiveBttnContinue -= OnInteractiveBttnContinue;
  }

  private void OnInteractiveBttnContinue()
  {
    _button.SetActive(true);
  }
}
