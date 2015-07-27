using UnityEngine;
using System.Collections;

public class pickupProp : MonoBehaviour {

	public Transform seed;
	Rigidbody rb;
	GameObject shadow;
	public bool canBeDisrupted;
	bool containsSeed = true;
	public float resetTime, disruptForce;

	// Use this for initialization
	void Start () {
		rb = transform.GetChild (0).GetComponent<Rigidbody> ();
		shadow = transform.GetChild (1).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.transform.tag == "Player" && canBeDisrupted) {
			Push(col.transform.position);
		}
	}

	void Push(Vector3 disruptorPosition){
		canBeDisrupted = false;
		Vector3 pushForce; 
		if (transform.position.x - disruptorPosition.x > 0) {
			pushForce = Vector3.right;
		} else {
			pushForce = Vector3.left;
		}
		rb.AddForce ((pushForce) * disruptForce);
		Invoke ("Reset", resetTime);
		if (containsSeed) {
			SendSeed();
		}
	}

	void SendSeed(){
		containsSeed = false;
		shadow.SetActive (false);
		Instantiate(seed	, transform.position, Quaternion.Euler(Vector3.zero));
	}

	void Reset(){
		canBeDisrupted = true;
	}
}
