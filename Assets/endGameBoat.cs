using UnityEngine;
using System.Collections;

public class endGameBoat : MonoBehaviour {

	public GameObject persGM;
	public GameObject UIprompt;
	public Animator anim;
	public GameObject player;
	public Transform playerPosMarker;
	bool setPlayerLoc;
	public cameraFocusControls camFocus;
	public customCameraControls camCont;
	public float camDist, camHeight;
	public moveAway moveTree;
	public Rigidbody[] islandRBs;
	public float resetTime;
	public Transform thisCheckpoint;
	public GameObject[] otherCheckpoints;

	public GameObject plantingInventory, vineInventory;

	// Use this for initialization
	void OnEnable () {
		transform.rotation = Quaternion.Euler (new Vector3 (270, 0, 0));
		persGM = GameObject.Find ("persistentGM");
		vineInventory = persGM.transform.GetChild (0).GetChild (1).gameObject;
		plantingInventory =persGM.transform.GetChild (0).GetChild (5).gameObject;
	//	anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("X") && UIprompt.activeSelf == true) {
			StartAnimation();
		}if (setPlayerLoc) {
			player.transform.position = playerPosMarker.position;
			//camFocus.StandingOn(playerPosMarker.position.y);
			camCont.camDistMod = camDist;
			camCont.cameraHeightModifier = -camHeight;
		}
	}

	public void FinishedAnimation(){
		anim.speed = 0;
		camCont.camDistMod = 0;
		foreach (Rigidbody rb in islandRBs) {
			//rb.enabled = true;
			//GetComponent<Rigidbody> ().isKinematic = true;
		}

		camCont.cameraHeightModifier = 0;
		setPlayerLoc = false;
		player.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		player.GetComponent<PlayerMove> ().accel = 0;
		Invoke ("ResetMove", resetTime);
		moveTree.enabled = true;
		this.enabled = false;
		GameObject.Find ("TheDrink").GetComponent<Resetter> ().currentCheckpoint = thisCheckpoint;
		foreach (GameObject check in otherCheckpoints) {
			check.SetActive(false);
		}
	}

	void ResetMove(){
		player.GetComponent<PlayerMove> ().accel = 70;
		plantingInventory.SetActive (true);
		player.GetComponent<PlayerInventory> ().enabled = false;
		player.GetComponent<playerIslandGrow> ().enabled = true;
	}

	void OnTriggerEnter (Collider col){
		if (col.transform.tag == "Player") {
			UIprompt.SetActive(true);
		}
	}

	void OnTriggerExit (Collider col){
		if (col.transform.tag == "Player") {
			UIprompt.SetActive(false);
		}
	}

	void StartAnimation(){
		vineInventory.SetActive (false);
		setPlayerLoc = true;
		anim.SetTrigger ("grow");
	}
}
