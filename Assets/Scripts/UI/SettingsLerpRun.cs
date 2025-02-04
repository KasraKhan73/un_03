using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsLerpRun : MonoBehaviour
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Start()
    {
        _image.color = PlayerPrefs.GetInt("SettingsLerpRun", 0) == 1 ? Color.green : Color.yellow;
    }

    public void Change()
    {
        PlayerPrefs.SetInt("SettingsLerpRun", PlayerPrefs.GetInt("SettingsLerpRun", 0) == 1? 0 : 1);
        PlayerPrefs.Save();
        
        _image.color = PlayerPrefs.GetInt("SettingsLerpRun", 0) == 1 ? Color.green : Color.yellow;
    }
}
