using UnityEngine;
using System.Collections;

public class setupFallSequenceTrigger : MonoBehaviour {

	public GameObject finalDoor;
	public Rigidbody finalPathRB;
	public GameObject finalParticles;
	public GameObject[] checkpoints, fallTriggers;
	public GameObject fallAndDie;
	public treeFallTriggerScript boatTrigger;
	public persistentAudio persAud;

	// Use this for initialization
	void Start () {
		persAud = GameObject.Find ("persistentAudioGM").GetComponent<persistentAudio> ();
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
			fallAndDie.SetActive(false);
			boatTrigger.enabled = true;
			boatTrigger.rbsToActivate[0] = finalPathRB;
			boatTrigger.particles[0] = finalParticles;
			finalDoor.SetActive(true);
		}
		persAud.ToggleMute (false);
		persAud.targetClip = persAud.level4B;
	}
}
