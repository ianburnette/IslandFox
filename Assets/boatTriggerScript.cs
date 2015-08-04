using UnityEngine;
using System.Collections;

public class boatTriggerScript : MonoBehaviour {

	public GameObject originalLuna, boatLuna, endLuna;

	public GameObject clouds;
	public Rigidbody boat;
	public Transform boatRef;
	public float boatForce, switchTime, transportTime;
	public bool watch;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			TransportBoat();
		}if (watch == true) {
			if (boat.velocity == Vector3.zero){
				Invoke ("CheckToSwitch", 1f);
			}
		}
	}

	void CheckToSwitch(){
		if (boat.velocity == Vector3.zero && watch) {
			SwitchBack ();
		} else if (watch){
			Invoke ("CheckToSwitch", 1f);
		}
	}

	void SwitchBack(){
		boat.isKinematic = true;
		boat.useGravity = false;
		watch = false;
		boatLuna.SetActive (false);
		endLuna.SetActive (true);
		endLuna.gameObject.transform.parent = null;
		boat.GetComponent<boatCollider> ().enabled = true;
	}

	void OnTriggerEnter(Collider col){
		if (col.transform.tag == "Player") {
			clouds.SetActive(true);
			Invoke ("TransportBoat", transportTime);
		}
	}

	void TransportBoat(){
		boat.GetComponent<boatCollider> ().enabled = false;
		clouds.SetActive (true);
		originalLuna.SetActive (false);
		boatLuna.SetActive (true);
		watch = false;
		print ("transporting");
		boat.isKinematic = false;
		boat.useGravity = true;
		boat.transform.position = boatRef.position;
		boat.AddForce (Vector3.left * boatForce);
		Invoke ("SwitchBack", switchTime);
		watch = true;
		boat.GetComponent<boatControls> ().targetTargetHeight = 258.54f;
		boat.GetComponent<boatControls> ().targetHeight = 258.54f;
	}
}
