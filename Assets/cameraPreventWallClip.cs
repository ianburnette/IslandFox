using UnityEngine;
using System.Collections;

public class cameraPreventWallClip : MonoBehaviour {

	public Transform dolly, player;
	customLookAtTarget lookAt;
	customCameraControls camCont;
	Quaternion startRot;
	public float correctionSpeed;

	// Use this for initialization
	void Start () {
		startRot = transform.rotation;
		lookAt = GetComponent<customLookAtTarget> ();
		camCont = dolly.gameObject.GetComponent<customCameraControls> ();
	}
	
	// Update is called once per frame
	void Update () {
		CheckForPlayer ();
		Debug.DrawRay (player.transform.position, transform.position - player.transform.position);
		transform.position = dolly.transform.position;
	}

	void CheckForPlayer(){
		RaycastHit hit;

		if (Physics.Raycast (player.transform.position, player.transform.position - transform.position, out hit, 30f)) {
			if (hit.transform.tag != "MainCamera"){
				//dolly.transform.position = hit.point;
				print ("computing");
				//camCont.camDistMod = 
				//camCont.camDistMod -= correctionSpeed * Time.deltaTime;
			}else if (camCont.camDistMod < 0) {
				camCont.camDistMod += correctionSpeed * Time.deltaTime;
			}
		}

		if (Physics.Raycast (transform.position, player.transform.position - transform.position, out hit, 30f)) {
			if (hit.transform.tag != "Player" && transform.position.z < player.position.z - 1){
				camCont.camDistMod -= correctionSpeed * Time.deltaTime;
			}else if (camCont.camDistMod < 0) {
				camCont.camDistMod += correctionSpeed * Time.deltaTime;
			}
		}
	}
}
