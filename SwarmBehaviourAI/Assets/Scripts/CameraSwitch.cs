using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour {

	public Camera topCam;
	public Camera cornerCam1;
	public Camera cornerCam2;
	public Camera cornerCam3;
	public Camera cornerCam4;
	public List<Camera> cams;
	private int idx = 0;
	private bool initTopCamAtStart = false;

	// Use this for initialization
	void Start () {
		changeToTopCamera ();
		initTopCamAtStart = true;
		cams = new List<Camera> ();
		cams.AddRange(new Camera[]{topCam, cornerCam1, cornerCam2, cornerCam3, cornerCam4});

	}

	void Update(){
		if (Input.GetKeyDown ("space")) {
			changeCameraOnClick ();
		} else if (Input.GetKeyDown ("t")) {
			changeToTopCamera ();
		}
	}

	private void changeCameraOnClick(){

		Debug.Log (idx);
		cams[idx].enabled = false;
		cams [idx++].enabled = true;
		if (idx == 5) {
			idx = 0;
		}

	}

	private void changeToTopCamera(){
		if (initTopCamAtStart) {
			idx = 0;
		}
		topCam.enabled = true;
		cornerCam1.enabled = false;
		cornerCam2.enabled = false;
		cornerCam3.enabled = false;
		cornerCam4.enabled = false;
	}

}
