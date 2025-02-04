using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Prototype.AudioCore;
using SBabchuk;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
	public DirectionRotate directionRotate;//delete
	
	public Vector3 vel;//delete
	
	#region Vars

	[Header("Камера, що слідкує")]
	public Transform camera;
	
	[Header("Швидкість руху вперед"), SerializeField]
	public static float speed = 10;

	[Header("Мінімальна сила")]
	public float strength = 15;
	
	[Header("Сила за 1 предмет")]
	public float strengthAdd = 5;
	
	[Header("Швидкість руху в сторони при бігу"), SerializeField]
	private float speedRun = 1;
	
	[Header("Кофіцієнт відношення канвас до всіту"), SerializeField]
	private float kof;
	
	[Header("Швидкість руху в сторони при польоті"), SerializeField]
	private float speedFly = 0.2f;
	
	[Header("Налаштування польоту"), SerializeField]
	private FlySettings _flySettings;

	private Camera _camera;
	
	private Transform curObj;

	private Vector3 startPos;
	
	public Vector3 currentTouchPosition;
	
	[Header("Зміщення відносно тапа")]
	public Vector3 offset;

	private Rigidbody _rigidbody;

	private bool isRunning = true;

	private Tween twnRotate;

	[SerializeField] public static DirectionRotate _directionRotate;
	
	private float _lenghtSwipe;
	
	private PlaneSettings settings;
	
	private float currentHorizontalSpeed = 0;
	
	public float currentVerticalSpeed = 0;
	
	private Vector3 currentAngle;

	private PlayerController _playerController;

	public float gravity = 1;
	
	public bool useGravity;
	
	public float newGravity;

	[Header("Сила гравітації при невзятті крил")]
	public float gravityDown = 0.6f;

	public static bool IsTap;

	#endregion
	
	#region Subscribe
	private void OnEnable()
	{
		PlayerController.OnFlying += SwitchMovement;

		PlayerController.OnDropedPlane += Stoped;
	}

	private void OnDisable()
	{
		PlayerController.OnFlying -= SwitchMovement;
		
		PlayerController.OnDropedPlane -= Stoped;
	}
	#endregion

	private void Awake()
	{
		_playerController = GetComponent<PlayerController>();
		
		_rigidbody = GetComponent<Rigidbody>();
		
		_camera = Camera.main;

		settings = GetComponent<PlayerController>().settings;
		
		speed = 10;

		InitSliders();
	}

	private void FixedUpdate ()
	{
		if(Time.timeScale == 0)
			return;

		if (isRunning)
		{
			//Running();
		}
		else
		{
			//Flying();
			
			//Fly();
		}

		vel = _rigidbody.velocity;
	}

	private void Stoped()
	{
		speed = 0;
		
		_rigidbody.velocity = Vector3.zero;

		enabled = false;
	}
	
	private void SwitchMovement()
	{
		isRunning = false;

		currentAngle = _rigidbody.transform.eulerAngles;
		
		DOVirtual.DelayedCall(0.5f, () =>
		{
			useGravity = true;
		});

		AudioController.PlaySound("Fly", StreamGroup.FX, 0.3f,true);
		AudioController.PlaySound("1", StreamGroup.FX, 0.15f,true);
	}

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

			offset = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)) - currentTouchPosition;
			
			var newPosition = new Vector3(CheckLimits( offset.x * kof), 0, 0);

			curObj.position += newPosition;

			currentTouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
		}
		else if(curObj)
		{
			IsTap = false;
			
			curObj = null;

			offset = Vector3.zero;
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

	private void Flying()
	{
		if(Input.GetMouseButton(0)) 
		{
			IsTap = true;
			
			
		}
		else if(curObj)
		{
			IsTap = false;
		}
	}

	private void Update()
	{
		directionRotate = _directionRotate;

		vel = _rigidbody.velocity;
		
		if (isRunning) return;
		
		//CheckControlInput();
	}
	
	private void CheckControlInput()
	  {
		  if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || _directionRotate != DirectionRotate.None) return;
		  
		  currentHorizontalSpeed = Mathf.Lerp(currentHorizontalSpeed, 0, Time.deltaTime);
		  currentAngle = new Vector3(
			  Mathf.LerpAngle(currentAngle.x, _flySettings.rorateX, Time.deltaTime * _flySettings.speedRotateX),
			  Mathf.LerpAngle(currentAngle.y, 0, Time.deltaTime * _flySettings.speedRotateY),
			  Mathf.LerpAngle(currentAngle.z, 0, Time.deltaTime * 2 * _flySettings.speedRotateX));
	  }
	  
    private void Fly()
    {
		_rigidbody.velocity = new Vector3(currentHorizontalSpeed, _rigidbody.velocity.y, _rigidbody.velocity.z);

		if (useGravity)
		{
			_rigidbody.velocity -= new Vector3(0,  GetGravity(), 0);
		}

		//_rigidbody.transform.eulerAngles = currentAngle;
	}

    private float GetGravity()
    {
	    if(_playerController)
		    if (!PlayerController.canFly)
		    {
			    _flySettings.rorateX = 30;
			    
			    return _rigidbody.velocity.y < -20 ? 0 : gravityDown;
		    }

	    var level = PlayerPrefs.GetInt("Progress", 1);
	    var countLevels = 10; // к-сть рівнів
	    var value = 1.0f / countLevels; // 1 ділиться на к-сть рівнів
	    
	    newGravity = gravity * Time.deltaTime; //сила гравітації
	    newGravity *= (1 + (countLevels - level) * value); //враховуєм рівень

	    var maxParts = 6; // максимальна к-сть предметів в літаку
	    value = 3.0f / maxParts;
	    newGravity *= (1 + (maxParts - PlayerController.trashCount) * value);

	    newGravity = _rigidbody.velocity.y < (IsTap ? -3 : -5) ? 0 : newGravity + (IsTap ? 0 : .3f);

	    return newGravity;
    }
	
	#region Test
	public void ChangeX()
	{
		_flySettings.rorateX = Slider.value;

		txt.text = ((int)_flySettings.rorateX).ToString();
	}
	public Slider Slider;
	public Text txt;
	public void minLenghtSwipe()
	{
		_flySettings.minLenghtSwipe = Slider.value;

		txt.text = (_flySettings.minLenghtSwipe).ToString("0.00");
	}
	public Slider Slider2;
	public Text txt2;
	public void minLenghtSwipeY()
	{
		_flySettings.minLenghtSwipeY = Slider2.value;

		txt2.text = (_flySettings.minLenghtSwipeY).ToString("0.00");
	}
	public Slider Slider3;
	public Text txt3;
	public void SpeedRun()
	{
		speedRun = Slider3.value;

		txt3.text = (speedRun).ToString("0.00");
	}
	private void InitSliders()
	{
		Slider.value = _flySettings.minLenghtSwipe;
		txt.text = (Slider.value).ToString("0.00");
		
		Slider2.value = _flySettings.minLenghtSwipeY;
		txt2.text = (Slider2.value).ToString("0.00");
		
		Slider3.value = speedRun;
		txt3.text = (Slider3.value).ToString("0.00");
	}

	#endregion
}

[System.Serializable]
public class FlySettings
{
	[Header("Максимальна горизонтальна швидкість")]
	public float maxHorizontalSpeed = 250;
	
	[Header("Максимальна горизонтальна швидкість без камери")]
	public float maxHorizontalSpeedWithoutCamera = 1;

	[Header("Кут по Х при нахилі літака"), Tooltip("Максимальний кут повороту по X")]
	public float rorateX = 0;
	[Header("Кут по Y при нахилі літака"), Tooltip("Максимальний кут повороту по Y")]
	public float rotateY = 10;
	[Header("Кут по Z при нахилі літака"), Tooltip("Максимальний кут повороту по Z")]
	public float rotateZ = 20;
	
	[Header("Максимальна довжина свайпа"), Tooltip("Після цієї довжини савйп вже не збільшує значення")]
	public float maxLenghtSwipe = 2;
	
	[Header("Мінімальна довжина свайпа"), Tooltip("Якщо менше цього значення, то не відбувається обрахунок")]
	public float minLenghtSwipe = 0.5f;
	
	[Header("Мінімальна довжина свайпа по Y"), Tooltip("Значення після якого починається поворот по Y")]
	public float minLenghtSwipeY = 0.5f;
	
	[Header("Швидкість поворота по X")]
	public float speedRotateX = 2;
	[Header("Швидкість поворота по Y")]
	public float speedRotateY = 4;
	[Header("Швидкість поворота по Z")]
	public float speedRotateZ = 2;
}
