using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class LikeController : MonoBehaviour
{
    public bool useStatic = true;
    
    private Camera _camera;

    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void Start()
    {
        _camera = Camera.main;
        
        _transform.localPosition = new Vector3(_transform.localPosition.x, _transform.localPosition.y, Random.Range(-0.4f, 0.4f));
    }

    private void LateUpdate()
    {
        if (!useStatic)
        {
            transform.LookAt(_camera.transform);
        }
        else
        {
            transform.rotation = _camera.transform.rotation;
        }

        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }

    public void Play()
    {
        _transform.DOLocalMoveY(_transform.localPosition.y + Random.Range(2.5f, 3f), 1.3f)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
    }
}
