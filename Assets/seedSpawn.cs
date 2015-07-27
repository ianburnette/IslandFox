using UnityEngine;
using System.Collections;

public class seedSpawn : MonoBehaviour {

	public Coin coinScript;
	Rigidbody rb;
	public float verticalVel, horizontalVelMin, horizontalVelMax, horizontalMult;
	public float resetTime;

	// Use this for initialization
	void OnEnable () {
		rb = GetComponent<Rigidbody> ();
		coinScript = transform.GetChild (0).GetComponent<Coin> ();
		coinScript.enabled = false;
		Launch ();
		Invoke ("Reenable", resetTime);
	}

	void Launch(){
		rb.AddForce (Vector3.up * verticalVel);
		Vector2 lateralVel;
		if (Random.value > .5f) {
			lateralVel.x = Random.Range (horizontalVelMin, horizontalVelMax);
		} else {
			lateralVel.x = Random.Range (-horizontalVelMin, -horizontalVelMax);
		}
		if (Random.value > .5f) {
			lateralVel.y = Random.Range (horizontalVelMin, horizontalVelMax);
		} else {
			lateralVel.y = Random.Range (-horizontalVelMin, -horizontalVelMax);
		}
		rb.AddForce (lateralVel * horizontalMult);
	}

	void Reenable(){
		coinScript.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col){
		if (col.transform.tag == "Player") {
			foreach(Transform child in transform){
				child.parent = null;
			}
			Reenable();
			Destroy(gameObject);
		}
	}
}
