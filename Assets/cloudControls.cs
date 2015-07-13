using UnityEngine;
using System.Collections;

public class cloudControls : MonoBehaviour {

	public float speed;
	public float maxVelocity;
	public float deadZone = .2f;
	public float h, v;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	void Update () {
		GetInput ();
		MoveDirection ();
		LimitSpeed ();
	}

	void LimitSpeed(){
		rb.velocity = Vector3.ClampMagnitude (rb.velocity, maxVelocity);
	}
	
	void GetInput(){
		h = Input.GetAxis ("Horizontal");
		v = Input.GetAxis ("Vertical");
	}

	void MoveDirection(){
		Vector3 moveDir = new Vector3 (h, 0, v);
		rb.AddForce(new Vector3(moveDir.x * speed, 0f, moveDir.z * speed));
	}
}
