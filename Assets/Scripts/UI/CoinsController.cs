using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoinsController : MonoBehaviour
{
    public GameObject panel;
    
    private void OnEnable()
    {
        PlayerController.OnFlying += Hide;
        
        PlayerController.OnShowResult += Show;
    }

    private void OnDisable()
    {
        PlayerController.OnFlying -= Hide;
        
        PlayerController.OnShowResult -= Show;
    }
    
    private void Hide()
    {
        panel.SetActive(false);
    }
    
    private void Show(float delay, int distance)
    {
        DOVirtual.DelayedCall(delay, () => { panel.SetActive(true); });
    }
}
