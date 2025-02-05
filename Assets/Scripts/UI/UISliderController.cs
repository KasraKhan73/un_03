using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UISliderController : MonoBehaviour
{
    public List<float> values;
    
    private Slider _slider;
    
    private Tween twn;
    private void OnEnable()
    {
        PlayerController.OnChangeSlider += Change;

        PlayerController.OnFlying += Hide;
    }

    private void OnDisable()
    {
        PlayerController.OnChangeSlider -= Change;

        PlayerController.OnFlying -= Hide;
    }

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }
    
    private void Start()
    {
        Change(0);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Change(int value = -1)
    {
        twn?.Kill();
        
        var newValue = 0.072f * value;

        twn =  _slider.DOValue(newValue, 1.0f).SetDelay(0.5f);
    }
}
