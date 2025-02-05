using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement Instance;
    
    public bool isFly = false;
    
    [Header("Налаштування бігу за гравцем")]
    public float cameraSpeedRun = 0.5f;
    
    [Header("Налаштування польоту за гравцем")]
    public Vector3 cameraOffset;
    
    public Transform player;
    
    private Camera _camera;

    private bool isStopedFollow = false;
    
    public float fieldOfView = 30;
    
    public float cameraSpeed = 0.1f;

    public float hightFly = 0;

    public float angleFly = 0;
    
    [Header("Налаштування при падінні гравця")]
    public Vector3 cameraOffsetFall;
    
    public float fieldOfViewFall = 20;
    
    public float cameraSpeedFall = 0.1f;

    private bool isFall;
    
    private void OnEnable()
    {
        PlayerController.OnFlying += ChangeMove;

        PlayerController.OnDropedPlane += Stop;
    }

    private void OnDisable()
    {
        PlayerController.OnFlying -= ChangeMove;
        
        PlayerController.OnDropedPlane -= Stop;
    }

    private void Awake()
    {
        Instance = this;
        
        _camera = GetComponent<Camera>();
    }

    public void Init()
    {
        transform.position = player.position + cameraOffset;
        
        Follow();
    }

    private void Stop()
    {
        isStopedFollow = false;
    }
    
    private void Follow()
    {
        isStopedFollow = true;
    }

    private void ChangeMove()
    {
        if (PlayerController.canFly)
        {
            isFly = true;
            
            SetSettingsFly();
        }
        else
        {
            SetSettingsFall();
        }
        
        //cameraOffset += new Vector3(0,hightFly, 0);
            
        //_camera.DOFieldOfView(fieldOfView, 0.3f);

        //_camera.transform.DORotate(_camera.transform.eulerAngles + new Vector3( angleFly , 0, 0), 0.3f);
    }

    private void SetSettingsFly()
    {
        cameraOffset += new Vector3(0,hightFly, 0);
            
        _camera.DOFieldOfView(fieldOfView, 0.3f);

        _camera.transform.DORotate(_camera.transform.eulerAngles + new Vector3( angleFly , 0, 0), 0.3f);
    }
    
    private void SetSettingsFall()
    {
        isFall = true;
        
        _camera.DOFieldOfView(fieldOfViewFall, 0.7f).OnComplete(() =>
        {
            //_camera.DOFieldOfView(10, 0.6f);
        });
        
        cameraOffsetFall = cameraOffset + new Vector3(10,hightFly, 0);
    }

    private void LateUpdate()
    {
        if(!isStopedFollow)
            return;
        
        if (isFly)
        {
            Fly();
        }
        else if (isFall)
        {
            Fall();
        }
        else
        {
            Move();
        }
    }
    
    private void Fall()
    { 
        //transform.position = player.position + cameraOffset;
        
        //var finalPosition = player.position + cameraOffset;
        //var lerpPosition = Vector3.Lerp (transform.position, finalPosition, cameraSpeed);
        //transform.position = new Vector3(finalPosition.x, lerpPosition.y, finalPosition.z);

        transform.position = Vector3.Lerp(transform.position, player.position + cameraOffsetFall, Time.deltaTime * cameraSpeedFall);
        
        transform.LookAt(player);
    }

    public float distance;
    public float direction;
    public float freezZoneX = 0.5f;
    
    private void Fly()
    {
        var newPosition = player.position + cameraOffset; //нова позиція

        transform.position = newPosition;
    }

    private void Move()
    {
        var newPosition = player.position + cameraOffset; //нова позиція
        
        //var finalPosition = player.position + cameraOffset;
        //var lerpPosition = Vector3.Lerp (transform.position, finalPosition, Time.deltaTime * cameraSpeedRun);
        //transform.position = new Vector3(lerpPosition.x, finalPosition.y, finalPosition.z);
        
        transform.position = newPosition;
    }
}
