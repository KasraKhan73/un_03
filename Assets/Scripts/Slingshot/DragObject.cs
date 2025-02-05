using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
	public static event Action OnImpulse;
	
	private Vector3 screenPoint;
	public Vector3 offset;
	private Vector3 scanPos;

	private Transform trans;

	private void Start()
	{
		trans = this.transform;
		
		scanPos = trans.position;

		_sensitivity = 40f;
		_rotation = Vector3.zero;
	}
	
	void Update()
	{
		SetDirection ();
	}

	private void OnMouseDown()
	{
		// rotating flag
		_isRotating = true;

		// store mouse
		_mouseReference = trans.position;

		screenPoint = Camera.main.WorldToScreenPoint(scanPos);

		Debug.Log(scanPos);
		
		Debug.Log(Camera.main.ScreenToWorldPoint(
			new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z)));
		
		offset = scanPos - Camera.main.ScreenToWorldPoint(
			new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

		//SlingshotController.Instance.aimer.eulerAngles = new Vector3 (0,0,0);
		//SlingshotController.Instance.setPath (true);
	}

	private void OnMouseDrag()
	{
		var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

		var curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

		var worldPoint = Camera.main.ScreenToWorldPoint(curScreenPoint);
		
		curPosition = new Vector3(trans.position.x, trans.position.y,  worldPoint.y - offset.y);
		
		trans.position = curPosition;

		var posX = Mathf.Clamp (trans.position.x,-1.4f,1.4f);
		var posZ = Mathf.Clamp (trans.position.z,-5f,0f);

		//trans.position = new Vector3 (posX, posY, curPosition.z);
		trans.position = new Vector3 (trans.position.x, trans.position.y, posZ);
	}
	void OnMouseUp()
	{
		// rotating flag
		_isRotating = false;
		
		OnImpulse?.Invoke();
		
		trans.position = scanPos;

		//SlingshotController.Instance.throwBall ();
		Invoke ("ResetDirection",1f);
	}

	void ResetDirection()
	{
		//SlingshotController.Instance.aimer.eulerAngles = new Vector3 (0,0,0);
		//SlingshotController.Instance.setPath (false);
		SlingshotController.Instance.ObjectHolder.GetComponent<Collider> ().enabled = true;
	}

	private float _sensitivity;
	private Vector3 _mouseReference;
	private Vector3 _mouseOffset;
	private Vector3 _rotation;
	private bool _isRotating;

	void SetDirection()
	{
		if(_isRotating)
		{
			// offset
			_mouseOffset = (trans.position - _mouseReference);

			// apply rotation
			_rotation.z = (_mouseOffset.x) * _sensitivity;

			// rotate
			//SlingshotController.Instance.aimer.Rotate (_rotation);

			// store mouse
			_mouseReference = trans.position;
		}
	}
}
