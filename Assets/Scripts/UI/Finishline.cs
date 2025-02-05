using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Finishline : MonoBehaviour
{
    private Text _text;

    private void OnEnable()
    {
        PlayerController.OnDropedPlane += Move;
    }

    private void OnDisable()
    {
        PlayerController.OnDropedPlane -= Move;
    }

    private void Awake()
    {
        _text = GetComponentInChildren<Text>();
    }
    
    private void Update()
    {
        _text.text = PlayerController.distanceOfFly+ "M";
    }

    private void Move()
    {
        transform.DOLocalMoveY(-375, 1);
    }
}
