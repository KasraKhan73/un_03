using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private int comboModNow;

    #region Subsribe
    private void OnEnable()
    {
        Zone._OnZoneEntered += OnZoneEntered;
    }
    private void OnDisable()
    {
        Zone._OnZoneEntered -= OnZoneEntered;
    }
    #endregion

    private void OnZoneEntered(int comboMod)
    {
        comboModNow = comboMod;
    }
    private int OnPlaneStoped()
    {
        return comboModNow;
    }
}
