using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour {

	public float sensitivity = 10f;
	public float maxYAngle = 80f;
	private Vector2 currentRot;


	// Update is called once per frame
	void Update () {
		currentRot.x += Input.GetAxis ("Mouse X") * sensitivity;
		currentRot.y -= Input.GetAxis ("Mouse Y") * sensitivity;
		currentRot.x = Mathf.Repeat (currentRot.x, 360);
		currentRot.y = Mathf.Clamp (currentRot.y, -maxYAngle, maxYAngle);
		transform.rotation = Quaternion.Euler (currentRot.y, currentRot.x, 0);		
	}
}
