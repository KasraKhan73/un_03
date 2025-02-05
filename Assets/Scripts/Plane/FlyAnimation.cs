using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FlyAnimation : MonoBehaviour
{
    public static FlyAnimation Instance;
    
    [Range(0, 2), Header("Зміщення по Х")]
    public float positionOffset = 0.5f;
    [Range(0, 10), Header("Швидкість")]
    public float positionOffsetSpeed = 3f;
    
    [Range(0, 45), Header("Поворот по Х"), Space(20)]
    public float rotateOffsetX = 30f;
    [Range(0, 10), Header("Швидкість")]
    public float rotateOffsetXSpeed = 3f;
    
    [Range(0, 45), Header("Поворот по Y"), Space(20)]
    public float rotateOffsetY = 10f;
    [Range(0, 10), Header("Швидкість")]
    public float rotateOffsetYSpeed = 3f;
    
    [Range(0, 45), Header("Поворот по Z"), Space(20)]
    public float rotateOffsetZ = 0f;
    [Range(0, 10), Header("Швидкість")]
    public float rotateOffsetZSpeed = 3f;

    public float defaultPositionOffset = 0.5f; 

    public float defaultPositionOffsetSpeed = 0.1f; 
    
    private Vector3 currentAngle;

    private float procents;

    private Vector3 newPosition;

    public bool isMoved;

    private float duration;

    public bool offLocalMove;

    public bool offLocalRotate;

    private void Awake()
    {
        Instance = this;
        
        positionOffset = PlayerPrefs.GetInt("UseValues", 0) == 0? defaultPositionOffset : PlayerPrefs.GetFloat("LimitsSlider", 0);
        
        positionOffsetSpeed = PlayerPrefs.GetInt("UseValues", 0) == 0? defaultPositionOffsetSpeed : PlayerPrefs.GetFloat("LocalFlySpeedSlider", 0);

    }

    private void Start()
    {
        currentAngle = transform.eulerAngles;
    }
  
    private void FixedUpdate()
    {
        if(!offLocalRotate)
        Rotate();
        
        if(!offLocalMove)
        if(isMoved)
            Move();
    }

    public void Falling(float value)
    {
        rotateOffsetX = -value;
    }
    
    public void Change(float value = 1, bool moved = false, float newDuration = 1)
    {
        procents = -value;

        isMoved = moved;

        duration = newDuration;
        
        newPosition = new Vector3(-positionOffset * (procents < 0? -1 : 1), transform.localPosition.y, transform.localPosition.z);
    }
    
    public void Back(float value = 1, bool moved = false, float newDuration = 1)
    {
        procents = -value;

        isMoved = moved;

        duration = newDuration;
        
        newPosition = new Vector3(-positionOffset * procents, transform.localPosition.y, transform.localPosition.z);
    }
    
    private void Rotate()
    {
        currentAngle = new Vector3(
            Mathf.LerpAngle(currentAngle.x, rotateOffsetX, Time.deltaTime * rotateOffsetXSpeed * duration), 
            Mathf.LerpAngle(currentAngle.y, -rotateOffsetY * procents, Time.deltaTime * rotateOffsetYSpeed * duration), 
            Mathf.LerpAngle(currentAngle.z, rotateOffsetZ * procents, Time.deltaTime * rotateOffsetZSpeed * duration));

        transform.eulerAngles = currentAngle;
    }

    private void Move()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition,newPosition, Time.deltaTime * positionOffsetSpeed);
    }
}
