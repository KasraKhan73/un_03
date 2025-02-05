using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Prototype.AudioCore;
using UnityEngine;

public class FlyScoreController : MonoBehaviour
{
    public static event Action<int> OnChangeScore;
    
    public float duration = 0.5f;
    
    public float durationUI = 0.5f;
    
    public AnimationCurve curveX;

    public AnimationCurve curveY;
    
    public AnimationCurve curveXUI;

    public AnimationCurve curveYUI;

    public int score;

    private void OnEnable()
    { 
    }
   
    private void OnDisable()
    {
    }

    private void TimeControllerOnOnTimeUp(bool obj)
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        Move(transform.position + new Vector3(3, 2, 0));
    }

    private void Move(Vector2 pos)
    {
        AudioController.PlaySound("Win");
        
        MoveToUI(UIScoreController.GetPosition());
        return;
        
        transform.DOMoveX(pos.x, duration)
            .SetEase(curveX).OnStart(() =>
            {
                transform.DOMoveY(pos.y, duration)
                    .SetEase(curveY);
            }).OnComplete(() =>
            {
                MoveToUI(UIScoreController.GetPosition());
            });
    }

    private void MoveToUI(Vector2 pos)
    {
        transform.DOMoveX(pos.x, durationUI)
            .SetEase(curveXUI).OnStart(() =>
            {
                transform.DOMoveY(pos.y, durationUI)
                    .SetEase(curveYUI);
            }).OnComplete(() =>
            {
                if(score > 0)
                    OnChangeScore?.Invoke(score);
                
                Destroy(gameObject);
            });
    }
}
