using UnityEngine;
using System.Collections;

public class setupFallSequenceTrigger : MonoBehaviour {

	public GameObject finalDoor;
	public Rigidbody finalPathRB;
	public GameObject[] checkpoints, fallTriggers;
	public GameObject fallAndDie;
	public treeFallTriggerScript boatTrigger;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider col) {
		if (col.transform.tag == "Player") {
			foreach (GameObject check in checkpoints){
				check.SetActive(false);
			}
			foreach (GameObject trigger in fallTriggers){
				trigger.SetActive(true);
			}
			fallAndDie.SetActive(true);
			boatTrigger.enabled = true;
			boatTrigger.rbsToActivate[0] = finalPathRB;
			finalDoor.SetActive(true);
		}
	}
}
