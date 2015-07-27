using UnityEngine;
using System.Collections;

public class boatControls : MonoBehaviour {

	public Animator anim;
	public Transform hull, sprite;
	public float speed, strokeForce, deadZone, maxVelocity, launchVelocity, turnSpeed;
	private float baseSpeed;
	public float h, v;
	public Rigidbody rb;
	public Transform spriteLocation;

	// Use this for initialization
	void Start () {
		baseSpeed = speed;
		rb = GetComponent<Rigidbody> ();
		sprite.parent = null;
		//anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		GetInput ();
		GetStroke ();
		MoveDirection ();
		UpdateAnimator ();
		LimitSpeed ();
	}

	void UpdateAnimator(){
		anim.SetFloat("yRotation", transform.rotation.eulerAngles.y);
		sprite.position = spriteLocation.position;
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

		SetRotation ();

		if (moveDir != Vector3.zero) {
			if (Mathf.Abs (moveDir.x) > deadZone)
				anim.SetFloat ("x", moveDir.x);
			if (Mathf.Abs (moveDir.z) > deadZone)
				anim.SetFloat ("v", moveDir.z);
		}
	}

	void SetRotation(){
		Vector3 lookVector = new Vector3 (transform.position.x + rb.velocity.x, transform.position.y + 2000f, transform.position.z + rb.velocity.z);


		//hull.transform.LookAt (lookVector);
		//hull.transform.rotation = Quaternion.Euler (hull.transform.rotation.eulerAngles.x, hull.transform.rotation.eulerAngles.y, 0);

		var lookPos = lookVector - transform.position;
		//lookPos.z = 0;

		var rotation = Quaternion.LookRotation(lookPos);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
	}

	void OnDrawGizmos(){
		Gizmos.DrawSphere (new Vector3(transform.position.x + rb.velocity.x, transform.position.y+2f, transform.position.z+rb.velocity.z), .4f);
	}

}
