using UnityEngine;
using System.Collections;

public class progressListenerScript : MonoBehaviour {

	public levelManager levelMan;

	// Use this for initialization
	void Start () {
		levelMan = GameObject.Find ("persistentGM").GetComponent<levelManager> ();

		//GameObject.DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.transform.tag == "Player") {
			levelMan.reachedTree = true;
		}
	}
}
