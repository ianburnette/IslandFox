using UnityEngine;
using System.Collections;

public class boatCollider : MonoBehaviour {

	bool inBoat = false;
	public Transform player;
	public float pushHeight;
	boatControls controls;
	public GameObject inBoatSprite;

	// Use this for initialization
	void Start () {
		controls = GetComponent<boatControls> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Jump") && inBoat) {
			EnablePlayer ();
		}
	}

	void DisablePlayer(){
		inBoat = true;
		player.position = transform.position;
		player.transform.parent = transform;
		player.gameObject.SetActive (false);
		controls.enabled = true;
		inBoatSprite.SetActive(true);
	}

	void EnablePlayer(){
		GetComponent<SphereCollider> ().enabled = false;
		inBoat = false;
		player.position = transform.position;
		player.transform.parent = null;
		player.gameObject.SetActive (true);
		player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
		GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
		//player.GetComponent<Rigidbody>().AddForce (Vector3.up * pushHeight, ForceMode.VelocityChange);
		Invoke ("EnableTrigger", 1f);
		controls.enabled = false;
		inBoatSprite.SetActive(false);
	}

	void EnableTrigger(){
		GetComponent<SphereCollider> ().enabled = true;
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player") {
			DisablePlayer();
		}
	}

}
