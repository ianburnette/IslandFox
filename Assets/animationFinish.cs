using UnityEngine;
using System.Collections;

public class animationFinish : MonoBehaviour {

	public endGameBoat endScript;
	public Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Finish(){
		endScript.FinishedAnimation ();
		anim.enabled = false;
	}
}
