using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TestSettings : MonoBehaviour
{
    public Text txtBttnChange;

    public List<Button> buttons;
    
    public Slider speedRunnerSlider;
    public Text textSpeedRunnerSlider;
    
    public Slider sensitiveTouchSlider;
    public Text textSensitiveTouchSlider;

    public Slider limitsSlider;
    public Text textlimitsSlider;
    
    public Slider localFlySpeedSlider;
    public Text textLocalFlySpeedSlider;
    
    public Slider backSpeedSlider;
    public Text textBackSpeedSlider;


    private float currentSpeedRunner;
    private float currentSensitiveTouch;
    private float currentLimitsFly;
    private float currentLocalFlySpeed;
    private float currentBackSpeed;

    private Button selected;
    
    private void Start()
    {
        InitSliders();
        
        UpdateButtons();
    }
    
    #region Методи для тестування
   
    public void SetSpeedRunner()
    {
        MovementController.Instance.speed = speedRunnerSlider.value;
        
        textSpeedRunnerSlider.text = (speedRunnerSlider.value).ToString("0.00");

        if(PlayerPrefs.GetInt("UseValues", 0) == 2)
            currentSpeedRunner = speedRunnerSlider.value;
    }
  
    public void SetSensitiveTouch()
    {
        MovementController.Instance.kof = sensitiveTouchSlider.value;
        
        textSensitiveTouchSlider.text = (sensitiveTouchSlider.value).ToString("0.00");
        
        if(PlayerPrefs.GetInt("UseValues", 0) == 2)
            currentSensitiveTouch = sensitiveTouchSlider.value;
    }
    
    public void SetLimitsFly()
    {
        FlyAnimation.Instance.positionOffset = limitsSlider.value;
        
        textlimitsSlider.text = (limitsSlider.value).ToString("0.00");

        if(PlayerPrefs.GetInt("UseValues", 0) == 2)
            currentLimitsFly = limitsSlider.value;
    }
    
    public void SetLocalSpeedFly()
    {
        FlyAnimation.Instance.positionOffsetSpeed = localFlySpeedSlider.value;
        
        textLocalFlySpeedSlider.text = (localFlySpeedSlider.value).ToString("0.00");

        if(PlayerPrefs.GetInt("UseValues", 0) == 2)
            currentLocalFlySpeed = localFlySpeedSlider.value;
    }
    
    public void SetBackSpeedFly()
    {
        MovementController.Instance.backSpeed = backSpeedSlider.value;
        
        textBackSpeedSlider.text = (backSpeedSlider.value).ToString("0.00");

        if(PlayerPrefs.GetInt("UseValues", 0) == 2)
            currentBackSpeed = backSpeedSlider.value;
    }

    private void UpdateButtons()
    {
        if (selected)
        {
            selected.interactable = true;
            
            InitSliders();
            
            SetSpeedRunner();

            SetSensitiveTouch();

            SetLimitsFly();

            SetLocalSpeedFly();
            
            SetBackSpeedFly();
        }

        selected = buttons[PlayerPrefs.GetInt("UseValues", 0)];
        selected.interactable = false;
    }
    
    private void InitSliders()
    {
        speedRunnerSlider.value = PlayerPrefs.GetInt("UseValues", 0) == 0? 
            MovementController.Instance.defaultSpeed 
            : 
            PlayerPrefs.GetInt("UseValues", 0) == 1? 
                PlayerPrefs.GetFloat("SpeedRunnerSlider", 0) 
                : 
                currentSpeedRunner;
        textSpeedRunnerSlider.text = (speedRunnerSlider.value).ToString("0.00");
        
        sensitiveTouchSlider.value = PlayerPrefs.GetInt("UseValues", 0) == 0? 
            MovementController.Instance.defaultKof 
            : 
            PlayerPrefs.GetInt("UseValues", 0) == 1? 
                PlayerPrefs.GetFloat("SensitiveTouchSlider", 0)
                :
                currentSensitiveTouch;
        textSensitiveTouchSlider.text = (sensitiveTouchSlider.value).ToString("0.00");
        
        limitsSlider.value = PlayerPrefs.GetInt("UseValues", 0) == 0? 
            FlyAnimation.Instance.defaultPositionOffset 
            : 
            PlayerPrefs.GetInt("UseValues", 0) == 1? 
                PlayerPrefs.GetFloat("LimitsSlider", 0)
                :
                currentLimitsFly;
        textlimitsSlider.text = (limitsSlider.value).ToString("0.00");
        
        localFlySpeedSlider.value = PlayerPrefs.GetInt("UseValues", 0) == 0? 
            FlyAnimation.Instance.defaultPositionOffsetSpeed 
            : 
            PlayerPrefs.GetInt("UseValues", 0) == 1? 
                PlayerPrefs.GetFloat("LocalFlySpeedSlider", 0)
                :
                currentLocalFlySpeed;
        textLocalFlySpeedSlider.text = (localFlySpeedSlider.value).ToString("0.00");
        
        backSpeedSlider.value = PlayerPrefs.GetInt("UseValues", 0) == 0? 
            MovementController.Instance.defaultBackSpeed 
            : 
            PlayerPrefs.GetInt("UseValues", 0) == 1? 
                PlayerPrefs.GetFloat("BackSpeed", 0)
                :
                currentBackSpeed;
        textBackSpeedSlider.text = (backSpeedSlider.value).ToString("0.00");
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("SpeedRunnerSlider", speedRunnerSlider.value);
        PlayerPrefs.SetFloat("SensitiveTouchSlider", sensitiveTouchSlider.value);
        PlayerPrefs.SetFloat("LimitsSlider", limitsSlider.value);
        PlayerPrefs.SetFloat("LocalFlySpeedSlider", localFlySpeedSlider.value);
        PlayerPrefs.SetFloat("BackSpeed", backSpeedSlider.value);
        PlayerPrefs.Save();
    }

    public void UseDefaultValues()
    {
        PlayerPrefs.SetInt("UseValues", 0);
        PlayerPrefs.Save();

        UpdateButtons();
    }
    
    public void UseSaveValues()
    {
        PlayerPrefs.SetInt("UseValues", 1);
        PlayerPrefs.Save();

        UpdateButtons();
    }
    
    public void UseCurrentValues()
    {
        PlayerPrefs.SetInt("UseValues", 2);
        PlayerPrefs.Save();

        UpdateButtons();
    }

    public void OffMove()
    {
        MovementController.Instance.offMove = !MovementController.Instance.offMove;
    }
    public void OffLocalMove()
    {
        FlyAnimation.Instance.offLocalMove = !FlyAnimation.Instance.offLocalMove;
    }
    public void OffLocalRotate()
    {
        FlyAnimation.Instance.offLocalRotate = !FlyAnimation.Instance.offLocalRotate;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ClearConsole();
        }
    }

    private static void ClearConsole()
    {
        var logEntries = System.Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");
 
        var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
 
        clearMethod.Invoke(null, null);
    }
    #endregion
}
