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
			//Respawn();
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	void Respawn(){
		transform.position = currentCheckpoint.position;
		rb.velocity = Vector3.zero;
	}

}
