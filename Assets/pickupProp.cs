using UnityEngine;
using System.Collections;

public class pickupProp : MonoBehaviour {

	public int seedSubType;

	public Transform seed;
	Rigidbody rb;
	GameObject shadow;
	public bool canBeDisrupted;
	public bool containsSeed = true;
	public float resetTime, disruptForce;
	public bool zDisrupt;
	public Animator anim;

	// Use this for initialization
	void Start () {
		//rb = transform.GetChild (0).GetComponent<Rigidbody> ();
		shadow = transform.GetChild (1).gameObject;
		if (transform.name != "fence1" && transform.name != "fence2") {
			anim = GetComponent<Animator> ();
		}
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
//		Vector3 pushForce; 
//		if (zDisrupt) {
//			if (transform.position.z - disruptorPosition.z > 0) {
//				pushForce = Vector3.forward;
//			} else {
//				pushForce = Vector3.back;
//			}
//		} else {
//			if (transform.position.x - disruptorPosition.x > 0) {
//				pushForce = Vector3.right;
//			} else {
//				pushForce = Vector3.left;
//			}
//		}
//		rb.AddForce ((pushForce) * disruptForce);
		//print ("pushing");
		if (transform.name != "fence1" && transform.name != "fence2") {
			if (disruptorPosition.x > transform.position.x) {
				anim.SetTrigger ("bounceR");
			} else {
				anim.SetTrigger ("bounceL");
			}

			Invoke ("Reset", resetTime);
		}
		if (containsSeed) {
			SendSeed();
		}
	}

	void SendSeed(){
		containsSeed = false;
		shadow.SetActive (false);
		Transform newSeed = (Transform)Instantiate(seed, transform.position, Quaternion.Euler(Vector3.zero));
		newSeed.GetComponent<selectSprite> ().SetSprite (seedSubType);
		newSeed.GetComponent<Coin> ().seedSubType = seedSubType;
	}

	void Reset(){
		canBeDisrupted = true;
	}
}
