using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public int levelToLoad;
	public levelManager levelMan;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.transform.tag == "Player") {
			levelMan.ChangeLevel(levelToLoad);
		}
	}
}
