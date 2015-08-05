using UnityEngine;
using System.Collections;

public class PathFirst : MonoBehaviour {

	public float pathTime;
//	public GameObject camTarget;

	// Use this for initialization
	void Start () {
		GetComponent<customCameraControls> ().enabled = false;
		iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("camPath"), "time", pathTime, "orientToPath", true, "looktime", 30f));
		Invoke ("camControlsOn", pathTime);
	}

	void camControlsOn(){
		GetComponent<customCameraControls> ().enabled = true;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
