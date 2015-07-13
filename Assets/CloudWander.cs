using UnityEngine;
using System.Collections;

public class CloudWander : MonoBehaviour {

	Rigidbody rb;
	public float speed, maxVelocity;
	public Vector3 direction;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		rb.AddForce (direction * speed * Time.deltaTime);
		LimitSpeed ();
	}

	void LimitSpeed(){
		rb.velocity = Vector3.ClampMagnitude (rb.velocity, maxVelocity);
	}
}
