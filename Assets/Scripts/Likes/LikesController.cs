using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LikesController : MonoBehaviour
{
    public List<LikeController> likes;
    
    private void Start()
    {
        StartCoroutine(ExampleCoroutine());
    }
    
    private IEnumerator ExampleCoroutine()
    {
        foreach (var like in likes)
        {
            like.Play();
            
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
        }
    }
}
