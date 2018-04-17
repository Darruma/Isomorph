using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour {

	public Vector3 centerPosition;
	public GameObject Is;
	void Start () {
		
	}
	

	void Update () {
		Is.transform.position = Vector3.Lerp (Is.transform.position, centerPosition, Time.deltaTime * 2);
	}

	public void startProject()
	{
		Application.LoadLevel (1);
	}
}
