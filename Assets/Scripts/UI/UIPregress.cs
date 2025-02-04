using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPregress : MonoBehaviour
{
    private Text _text;

    private void Awake()
    {
        _text = GetComponentInChildren<Text>();
    }
    
    private void Update()
    {
        _text.text = PlayerController.distanceOfFly + "M";
    }
}
