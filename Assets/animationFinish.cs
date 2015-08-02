using UnityEngine;
using System.Collections;

public class animationFinish : MonoBehaviour {

	public endGameBoat endScript;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Finish(){
		endScript.FinishedAnimation ();
	}
}
