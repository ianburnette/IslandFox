using UnityEngine;
using System.Collections;

public class PathFirst : MonoBehaviour {

	public float pathTime;
//	public GameObject camTarget;

	public string[] paths;
	public int pathIndex=-1;

	

	// Use this for initialization
	void Start () {
		GetComponent<customCameraControls> ().enabled = false;
		InvokeRepeating ("NewPath", 0f, 2f);


		Invoke ("camControlsOn", pathTime);
	}
	void NewPath(){
		pathIndex++;
		if (pathIndex < 6) {
			//transform.position = iTweenPath.GetPath(paths[pathIndex]).
			iTween.MoveTo (gameObject, iTween.Hash (
			"path", iTweenPath.GetPath (paths[pathIndex]),
			"time", pathTime, 
			"easeType", "easeInOutQuad"
			));
		} else {
			camControlsOn();
		}
	}

	void camControlsOn(){
		GetComponent<customCameraControls> ().enabled = true;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
