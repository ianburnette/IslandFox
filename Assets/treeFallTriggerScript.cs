using UnityEngine;
using System.Collections;

public class treeFallTriggerScript : MonoBehaviour {

	public Rigidbody[] rbsToActivate;
	public GameObject[] particles;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider col) {
		if (col.transform.tag == "Player") {
			print ("activated");
			foreach (Rigidbody rb in rbsToActivate){
				rb.isKinematic = false;
				rb.transform.GetComponent<MeshCollider>().enabled = false;
				foreach (Transform child in rb.transform){
					if (child.GetComponent<MeshCollider>() != null){
						child.GetComponent<MeshCollider>().enabled = false;
					}
				}
			}
		}

		foreach (GameObject part in particles) {
			part.SetActive(true);
		}

	}
}
