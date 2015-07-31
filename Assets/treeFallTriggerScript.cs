using UnityEngine;
using System.Collections;

public class treeFallTriggerScript : MonoBehaviour {

	public Rigidbody[] rbsToActivate;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider col) {
		if (col.transform.tag == "Player") {
			foreach (Rigidbody rb in rbsToActivate){
				rb.isKinematic = false;
			}
		}
	}
}
