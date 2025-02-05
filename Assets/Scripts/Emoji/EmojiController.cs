using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EmojiController : MonoBehaviour
{
    public List<Transform> smiles;

    public Sprite good;

    public Sprite bad;
    
    private void OnDisable()
    {
        PlayerController.OnDropedPlane -= Create;
    }

    private void OnEnable()
    {
        PlayerController.OnDropedPlane += Create;
    }

    private void Create()
    {
        StartCoroutine(ExampleCoroutine());
    }
    
    private IEnumerator ExampleCoroutine()
    {
        foreach (var smile in smiles)
        {
            var scale = Random.Range(1.5f, 2f);

            smile.GetComponent<Image>().sprite = PlayerController.isFailed ? bad : good;
            
            smile.DOScale(new Vector3(scale, scale,scale), 0.5f)
                .SetLoops(-1, LoopType.Yoyo)
                .OnStart(() =>
                {
                    smile.DOLocalRotate(new Vector3(0, 0,Random.Range(-35, 35)), 0.5f);
                });
         
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
        }
      
        DOVirtual.DelayedCall(1f, () =>
        {
            Destroy(gameObject);
        });
    }
}
