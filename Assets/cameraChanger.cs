using UnityEngine;
using System.Collections;
using UnityStandardAssets.Cameras;

public class cameraChanger : MonoBehaviour {

	public Transform camFocus;
	public customLookAtTarget lookAt;
	public CameraFocus focusScript;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Change(Vector3 target, bool isPlayerFocused){
		//print ("changing");
		camFocus.position = target;
		if (isPlayerFocused) {
			focusScript.enabled = true;
			//lookAt.enabled = false;
		}
		else {
		//	lookAt.enabled = true;
			focusScript.enabled = false;
		}
	}
}
