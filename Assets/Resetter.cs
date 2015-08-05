using UnityEngine;
using System.Collections;

public class Resetter : MonoBehaviour {

	public Transform currentCheckpoint;
	public Transform player;
	public Rigidbody playerRB;
	public Transform[] checkpoints;

	// Use this for initialization
	void Start () {
		foreach (Transform check in checkpoints) {
			check.GetComponent<CheckpointSingle>().resetterScript = this;
		}
		player = GameObject.Find ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetCheckpoint(Transform newCheck){
		currentCheckpoint = newCheck;
	}

	void OnCollisionEnter(Collision col){
		if (col.transform.tag == "Player") {
			player = col.transform;
			Reset();
		}
	}

	public void Reset(){
		playerRB.velocity = Vector3.zero;
		player.transform.position = currentCheckpoint.position;
	}
}
