using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Keypad0)){
			Application.LoadLevel(0);
		}
		if (Input.GetKeyDown (KeyCode.Keypad1)){
			Application.LoadLevel(1);
		}
		if (Input.GetKeyDown (KeyCode.Keypad2)){
			Application.LoadLevel(2);
		}
		
		
	}
}
