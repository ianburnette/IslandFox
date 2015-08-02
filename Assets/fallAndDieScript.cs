using UnityEngine;
using System.Collections;

public class fallAndDieScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		if (col.transform.tag == "Player") {
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
