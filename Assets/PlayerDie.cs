using UnityEngine;
using System.Collections;

public class PlayerDie : MonoBehaviour {

	public Transform currentCheckpoint;
	public float minHeight;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < minHeight) {
			Respawn();
		}
	}

	void Respawn(){
		transform.position = currentCheckpoint.GetChild (0).position;
		rb.velocity = Vector3.zero;
	}

}
