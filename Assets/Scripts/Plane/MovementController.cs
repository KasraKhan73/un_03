using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Prototype.AudioCore;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum States
{
    None,
    Running,
    Falling,
    Flying
}

public class MovementController : MonoBehaviour
{
    #region Testing Vars

    public float Gr;
    public bool offMove;
    #endregion
    
    #region Public Vars

    public static MovementController Instance;
    
    public States state;

    public DirectionRotate direction;
    public DirectionRotate directionLast;
    
    [Range(0, 10), Header("Швидкість руху вперед при польоті в сторони")]
    public float maxSpeed;

    [Header("Камера, що слідкує")]
    public Transform camera;
    
    [Header("Швидкість руху вперед"), Range(0, 20)]
    public float defaultSpeed = 10; 

    [Header("Кофіцієнт відношення канвас до всіту"), Range(0, 10)]
    public float defaultKof = 2;
    
    [Header("Швидкість повернення в початок при відпусканні"), Range(0, 2)]
    public float backSpeed = 0.3f;

    [HideInInspector ] public float defaultBackSpeed = 0.3f;

    [Header("Зміщення відносно тапа")]
    public float offsetX;
    
    public float offsetXLast;

    public float offsetXCurrent;
    
    [Header("Довжина свайпа")]
    public float _lenghtSwipe;
    
    [Header("Довжина мінімального свайпа")]
    public float minSwipe;
    
    [Header("Довжина максимальна свайпа")]
    public float maxSwipe;
    
    [Header("Включеність гравітації")]
    public bool useGravity;
    
    [Header("Сила гравітації при невзятті крил")]
    public float gravityDown = 0.6f;
    
    [Header("Сила гравітацфї")]
    public float newGravity;
    
    [Header("Швидкість руху")]
    public float speed = 10;

    [Header("Кофіцієнт відношення канвас до всіту")]
    public float kof = 2;
    #endregion

    #region Private Vars
    private Rigidbody _rigidbody;
    
    private Transform curObj;
    
    private Transform tr;

    private Camera _camera;
    
    private Vector3 startPos;
    
    private Vector3 currentAngle;

    private Vector3 currentTouchPosition;

    private Vector3 newPosition;

    private float procents = 1;

    private FlyAnimation _animation;
   

    #endregion

    #region Static Vars
    public static bool IsTap;
    #endregion

    private void OnEnable()
    {
        PlayerController.OnFlying += SwitchState;
    }

    private void OnDisable()
    {
        PlayerController.OnFlying -= SwitchState;
    }
    
    private void Awake()
    {
        Instance = this;
        
        _rigidbody = GetComponent<Rigidbody>();
        
        _camera = Camera.main;

        _animation = GetComponentInChildren<FlyAnimation>();

        state = States.Running;

        speed = PlayerPrefs.GetInt("UseValues", 0) == 0? defaultSpeed : PlayerPrefs.GetFloat("SpeedRunnerSlider", 0);

        kof = PlayerPrefs.GetInt("UseValues", 0) == 0? defaultKof : PlayerPrefs.GetFloat("SensitiveTouchSlider", 0);;
    
        backSpeed = PlayerPrefs.GetInt("UseValues", 0) == 0? defaultBackSpeed : PlayerPrefs.GetFloat("BackSpeed", 0);
    
    }

    private void SwitchState ()
    {
        state = PlayerController.canFly ? States.Flying : States.Falling;

        if (state != States.Flying && state != States.Falling) return;
        
        DOVirtual.DelayedCall(0.3f  + 0.35f * PlayerPrefs.GetInt("Progress", 1), () =>
        {
            useGravity = true;
        });

        AudioController.PlaySound("Fly", StreamGroup.FX, 0.3f,true);
        AudioController.PlaySound("1", StreamGroup.FX, 0.15f,true);
    }
    
    private void FixedUpdate ()
    {
        switch (state)
        {
            case States.None:
                break;
            case States.Running:
                Running();
                break;
            case States.Falling:
                Falling();
                break;
            case States.Flying:
                Flying();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    #region Гравітація
    private void CheckGravity()
    {
        if (useGravity || !IsTap)
        {
            if (state == States.Falling)
            {
                _rigidbody.velocity -= new Vector3(0, GetGravity(), 0);
            }
            else
            {
                _rigidbody.velocity = Vector3.Lerp(
                    _rigidbody.velocity,
                    new Vector3(_rigidbody.velocity.x,  IsTap? -3 : -10, _rigidbody.velocity.z), 
                    SpeedChangeGravity());
            }
        }
    }

    private float SpeedChangeGravity()
    {
        newGravity = Time.deltaTime; //сила гравітації
        
        var maxParts = 14; // максимальна к-сть предметів в літаку
        var value = 7.0f / maxParts;
        newGravity *= (1 + (maxParts - PlayerController.trashCount) * value);
        
        //var level = PlayerPrefs.GetInt("Progress", 1);
        //var countLevels = 10; // к-сть рівнів
        //value = 10.0f / countLevels; // 1 ділиться на к-сть рівнів
        //newGravity *= (1 + (countLevels - level) * value); //враховуєм рівень
        Gr = newGravity;
        return newGravity;
    }
    
    private float GetGravity()
    {
        if (!PlayerController.canFly)
        {
            _animation.Falling(-30);
            
            return _rigidbody.velocity.y < -20 ? 0 : gravityDown;
        }

        var level = PlayerPrefs.GetInt("Progress", 1);
        var countLevels = 10; // к-сть рівнів
        var value = 1.0f / countLevels; // 1 ділиться на к-сть рівнів
	    
        newGravity = 0.5f * Time.deltaTime; //сила гравітації
        newGravity *= (1 + (countLevels - level) * value); //враховуєм рівень

        var maxParts = 14; // максимальна к-сть предметів в літаку
        value = 7.0f / maxParts;
        newGravity *= (1 + (maxParts - PlayerController.trashCount) * value);

        newGravity = _rigidbody.velocity.y < (IsTap ? -3 : -5) ? 0 : newGravity + (IsTap ? 0 : .3f);

        Debug.Log("newGravity: " + newGravity);
        return newGravity;
    }
    #endregion
   
    
    #region  Falling
    private void Falling()
    {
        _animation.Change();
        
        CheckGravity();
    }
    #endregion

    #region Flying

    private void Flying()
    {
        if (Input.GetMouseButton(0))
        {
            IsTap = true;
            
            _animation.Falling(0);
            
            if (!curObj)
            {
                curObj = gameObject.transform;

                startPos = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            }

            if (!curObj) return;

            offsetX = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)).x - startPos.x;
            
            _lenghtSwipe = Mathf.Abs(offsetX);

            _lenghtSwipe = _lenghtSwipe > maxSwipe ? maxSwipe : _lenghtSwipe;

            if (_lenghtSwipe > minSwipe)
            {
                offsetX = CheckMaxSpeed(kof * offsetX);
                
                if (offsetXLast != offsetX)
                {
                    direction = offsetXLast > offsetX ? DirectionRotate.Left : DirectionRotate.Right;

                    if(directionLast != DirectionRotate.None)
                        direction = directionLast == direction ? direction : DirectionRotate.Back;

                    directionLast = direction;

                    if(direction == DirectionRotate.Left || direction == DirectionRotate.Right)
                        offsetXCurrent = offsetXLast;
                    
                    offsetXLast = offsetX;
                }
            }
            else
            {
                direction = DirectionRotate.None;
                
                directionLast = direction;
                
                offsetX = 0;
                
                offsetXLast = offsetX;

                offsetXCurrent = 0;
            }

            procents = offsetX / maxSwipe;
            
            newPosition = new Vector3(curObj.position.x + offsetX + 5 * procents, curObj.position.y, curObj.position.z);

            if(!offMove)
                curObj.position = Vector3.Lerp(curObj.position, newPosition, Time.deltaTime);

            _animation.Change(procents, direction == DirectionRotate.Left || direction == DirectionRotate.Right);
        }
        else
        {
            if (curObj)
            {
                curObj = null;
                
                IsTap = false;
            
                _animation.Falling(-10);
            
                _animation.Back(0, true, backSpeed);
            }
        }

        CheckGravity();
    }
    private float CheckMaxSpeed(float value)
    {
        return Mathf.Abs(value) > maxSpeed ? maxSpeed * (value < 0? -1 : 1) : value;
    }
    #endregion
    
    
    #region Running
    private void Running()
    {
        _rigidbody.velocity = transform.forward * speed;
		
        if(Input.GetMouseButton(0))
        {
            IsTap = true;
			
            if (!curObj)
            {
                curObj = gameObject.transform;

                startPos = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

                currentTouchPosition = startPos;
            }

            if (!curObj) return;

            offsetX = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)).x - currentTouchPosition.x;
			
            var newPosition = new Vector3(CheckLimits(offsetX * kof), 0, 0);

            curObj.position += newPosition;

            currentTouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        }
        else if(curObj)
        {
            IsTap = false;
			
            curObj = null;
        }
    }
    
    private float CheckLimits(float newPositionX)
    {
        var position = newPositionX;
		
        if (curObj.position.x + position >= 1.1f)
        {
            position = 0;
        }
        else if (curObj.position.x + position <= -1.1f)
        {
            position = 0;
        }

        return position;
    }
    #endregion
}
