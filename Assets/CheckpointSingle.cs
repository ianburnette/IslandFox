using UnityEngine;
using System.Collections;

public class CheckpointSingle : MonoBehaviour {

	public Resetter resetterScript;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.transform.tag == "Player") {
			resetterScript.SetCheckpoint(transform);
		}
	}
}
