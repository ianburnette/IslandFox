using UnityEngine;
using System.Collections;

public class muteTrigger : MonoBehaviour {

	public persistentAudio persAud;

	// Use this for initialization
	void Start () {
		persAud = GameObject.Find ("persistentAudioGM").GetComponent<persistentAudio> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.transform.tag == "Player") {
			persAud.ToggleMute();
			Destroy (gameObject);
		}
	}
}
