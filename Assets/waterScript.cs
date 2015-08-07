using UnityEngine;
using System.Collections;

public class waterScript : MonoBehaviour {

	public GameObject splash, ripples;
	public GameObject myRipples;
	bool playerIn;
	public Transform player;
	public float vertOffset;

	// Use this for initialization
	void Start () {
		myRipples = (GameObject)Instantiate (ripples, transform.position, Quaternion.identity);
		myRipples.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (playerIn) {
			myRipples.transform.position = player.position + (Vector3.up * vertOffset);
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.transform.tag == "Player") {
			Instantiate (splash, col.transform.position, Quaternion.identity);
			player = col.transform;
			myRipples.gameObject.SetActive(true);
			playerIn=true;
		}
	}

	void OnTriggerExit(Collider col){
		if (col.transform.tag == "Player") {
			Instantiate (splash, col.transform.position, Quaternion.identity);
			myRipples.gameObject.SetActive(false);
			playerIn=false;
		}
	}
}
