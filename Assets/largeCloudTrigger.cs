using UnityEngine;
using System.Collections;

public class largeCloudTrigger : MonoBehaviour {

	public GameObject clouds;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.transform.tag == "Player") {
			clouds.SetActive(true);
			//Invoke ("TransportBoat", transportTime);
		}
	}

}
