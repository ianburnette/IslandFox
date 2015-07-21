using UnityEngine;
using System.Collections;

public class customCameraControls : MonoBehaviour {

	public Transform player, camFocus;

	public Vector3[] camVectors;
	public int cameraPosition; //1=close, 2=med, 3=far
	public int cameraFocusDirection = 3; //1=n, 2=s, 3=w, 4=e
	public float posChangeTime;
	public float currentCamHeight, currentCamDist, cameraHeightModifier;
	public bool inDialogue;
	private int stateToReturnTo = 2;
	public float heightModBase = 2f;

	// Use this for initialization
	void Start () {
		SetCameraPosition (1);
	}
	
	// Update is called once per frame
	void Update () {
		AlignWithFocus ();
		GetCameraInput ();
	}

	void AlignWithFocus(){
		transform.position = new Vector3(camFocus.position.x, camFocus.position.y + currentCamHeight - cameraHeightModifier,camFocus.position.z - currentCamDist);
	}

	public void Dialogue(bool state){
		inDialogue = state;
		if (state == true) {
			SetHeightModifier(heightModBase);
			SetCameraPosition(1);
		}if (state == false) {
			SetHeightModifier(0f);
			SetCameraPosition(stateToReturnTo);
		}
	}

	void SetHeightModifier (float newMod){
		iTween.ValueTo (gameObject, iTween.Hash(
			"from", cameraHeightModifier,
			"to", newMod,
			"time", posChangeTime,
			"onupdate", "UpdateHeight"
			));
	}

	void UpdateHeight(float newHeight){
		cameraHeightModifier = newHeight;
	}

	void GetCameraInput(){
		if (!inDialogue) {
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				SetCameraPosition (1);
				stateToReturnTo = 1;
			}
			if (Input.GetKeyDown (KeyCode.Alpha2)) {
				SetCameraPosition (2);
				stateToReturnTo = 3;
			}
			if (Input.GetKeyDown (KeyCode.Alpha3)) {
				SetCameraPosition (3);
				stateToReturnTo = 3;
			}
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
