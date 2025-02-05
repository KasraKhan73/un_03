using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PopUpFX : MonoBehaviour
{
    private void Start()
    {
        DOVirtual.DelayedCall(2, () =>
        {
            Destroy(this.gameObject);
        });
    }
}
