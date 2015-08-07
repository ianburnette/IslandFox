using UnityEngine;
using System.Collections;

public class PushRigidbody : MonoBehaviour {

	public float pushAmt, turnAmt, upVelocity;

	Rigidbody rb;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.AddForce ((Vector3.up * upVelocity) + (Vector3.left * Random.Range (-pushAmt, pushAmt)) + (Vector3.forward * Random.Range (-pushAmt, pushAmt)));
		rb.AddTorque (Vector3.up * Random.Range (-turnAmt, turnAmt));
		rb.AddTorque (Vector3.left * Random.Range (-turnAmt, turnAmt));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
