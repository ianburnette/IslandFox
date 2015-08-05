using UnityEngine;
using System.Collections;

public class boatActivateTut : MonoBehaviour {

	public GameObject tut;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.transform.tag == "Player") {
			tut.gameObject.SetActive(true);
		}
	}
}
