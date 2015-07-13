using UnityEngine;
using System.Collections;

public class boatControls : MonoBehaviour {

	public Animator anim;
	public float speed, strokeForce, deadZone, maxVelocity, launchVelocity;
	private float baseSpeed;
	public float h, v;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		baseSpeed = speed;
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		GetInput ();
		GetStroke ();
		MoveDirection ();
		LimitSpeed ();
	}

	public void OnCloud (bool on){
		if (on) {
			speed = 0;
		} else {
			speed = baseSpeed;
			LaunchStroke();
		}
	}

	void LimitSpeed(){
		rb.velocity = Vector3.ClampMagnitude (rb.velocity, maxVelocity);
	}

	void GetInput(){
		h = Input.GetAxis ("Horizontal");
		v = Input.GetAxis ("Vertical");
	}

	void GetStroke(){
		if (Input.GetMouseButtonDown (0)) {
			Stroke();
		}
	}

	void LaunchStroke(){
		rb.AddForce (Vector3.up * launchVelocity);
	}

	void Stroke(){
		rb.AddForce (Vector3.up * strokeForce);
	}

	void MoveDirection(){
		Vector3 moveDir = new Vector3 (h, 0, v);
		rb.AddForce(new Vector3(moveDir.x * speed, 0f, moveDir.z * speed));
		if (moveDir != Vector3.zero) {
			if (Mathf.Abs (moveDir.x) > deadZone)
				anim.SetFloat ("x", moveDir.x);
			if (Mathf.Abs (moveDir.z) > deadZone)
				anim.SetFloat ("v", moveDir.z);
		}
	}
}
