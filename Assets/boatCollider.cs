using UnityEngine;
using System.Collections;

public class boatCollider : MonoBehaviour {

	bool inBoat = false;
	public Transform player;
	public Transform spriteLocation;
	public float pushHeight;
	boatControls controls;
	public GameObject inBoatSprite;
	public float targetHeight, frontHeight, backHeight, targetTargetHeight, correctSpeed;
	public LayerMask mask;

	// Use this for initialization
	void Start () {
		controls = GetComponent<boatControls> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("A") && inBoat) {
			EnablePlayer ();
		}
		if (!inBoat) {
			CheckBelow();
		}
	}

	void CheckBelow(){
		RaycastHit hit;
		Debug.DrawRay (transform.position - (transform.up * 1.5f) + (transform.right / 2) + (transform.forward), Vector3.down * 50f);
		Debug.DrawRay (transform.position + (transform.up * 1.5f) + (transform.right / 2) + (transform.forward), Vector3.down * 50f);
		
		//float frontHeight, backHeight;
		
		bool hittingFront, hittingBack;
		
		if (Physics.Raycast (transform.position - (transform.up * 1.5f) + (transform.right / 2) + (transform.forward), Vector3.down, out hit, 50f, mask)) {
			//print ("boat hitting " + hit.transform);
			hittingFront = true;
			frontHeight = hit.point.y;
		} else {
			hittingFront = false;
		}
		if (Physics.Raycast (transform.position + (transform.up * 1.5f) + (transform.right / 2) + (transform.forward), Vector3.down, out hit, 50f, mask)) {
			//print ("boat hitting " + hit.transform);
			hittingBack = true;
			backHeight = hit.point.y;
		} else {
			hittingBack = false;
		}
		
		if (hittingFront == false && hittingBack == false) {
			targetHeight = targetTargetHeight;
		} else if (hittingFront && !hittingBack) {
			targetHeight = frontHeight;
		} else if (!hittingFront && hittingBack) {
			targetHeight = backHeight;
		} else {
			if (backHeight > frontHeight) {
				targetHeight = backHeight;
			} else {
				targetHeight = frontHeight;
			}
		}

		if (!hittingFront && !hittingBack) {
			targetHeight = targetTargetHeight;
		}

		if (transform.position.y > targetHeight ) {
			transform.position = Vector3.Lerp (transform.position, 
			                                   new Vector3 (transform.position.x, targetHeight, transform.position.z),
			                                   correctSpeed * Time.deltaTime);
			             					
			            
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
		player.position = spriteLocation.position;
		player.transform.parent = null;
		player.gameObject.SetActive (true);
		player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
		player.rotation = Quaternion.Euler (new Vector3(0,180,0));

		GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
		player.GetComponent<Rigidbody>().AddForce (Vector3.up * pushHeight, ForceMode.VelocityChange);
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
