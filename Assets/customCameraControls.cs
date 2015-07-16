using UnityEngine;
using System.Collections;

public class customCameraControls : MonoBehaviour {

	public Transform player, camFocus;

	public Vector3[] camVectors;
	public int cameraPosition; //1=close, 2=med, 3=far
	public int cameraFocusDirection = 3; //1=n, 2=s, 3=w, 4=e
	public float posChangeTime;
	public float currentCamHeight, currentCamDist;

	// Use this for initialization
	void Start () {
		SetCameraPosition (1);
	}
	
	// Update is called once per frame
	void Update () {
		AlignWithFocus ();
		GetCameraInput ();
		SetCamFocusPosition ();

	}

	void AlignWithFocus(){
		transform.position = new Vector3(camFocus.position.x, camFocus.position.y + currentCamHeight,camFocus.position.z - currentCamDist);
	}

	void SetCamFocusPosition(){

	}

	void GetCameraInput(){
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			SetCameraPosition(1);
		}if (Input.GetKeyDown (KeyCode.Alpha2)) {
			SetCameraPosition(2);
		}if (Input.GetKeyDown (KeyCode.Alpha3)) {
			SetCameraPosition(3);
		}

	}

	void SetCameraPosition(int pos){
		iTween.ValueTo (gameObject, iTween.Hash(
			"from", currentCamHeight,
			"to", camVectors [pos-1].y,
			"time", posChangeTime,
			"onupdate", "SetY"
			));
		iTween.ValueTo (gameObject, iTween.Hash(
			"from", currentCamDist,
			"to", camVectors [pos-1].z,
			"time", posChangeTime,
			"onupdate", "SetZ"
			));
	}

	void SetY(float newY){
		currentCamHeight = newY;
	}
	void SetZ(float newZ){
		currentCamDist = newZ;
	}
}
