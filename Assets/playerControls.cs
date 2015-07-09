using UnityEngine;
using System.Collections;

public class playerControls : MonoBehaviour {

	public float walkSpeed, groundCheckDist, centerOffset, jumpSpeed, jumpStopSpeed, gravityMultiplier;
	public LayerMask groundMask;
	public bool grounded;
	public CameraFocus focusScript;
	float h, v;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		GetInput ();
		GroundCheck ();
		ExtraGravity ();
		ApplyMovement ();
	}

	void GetInput(){
		h = Input.GetAxis ("Horizontal");
		v = Input.GetAxis ("Vertical");
		if (Input.GetButtonDown ("Jump"))
			Jump ();
		if (Input.GetButtonUp ("Jump"))
			StopJump ();
	}

	bool CheckRay(int dir){
		bool result;
		RaycastHit hit;
		Vector3 directionalModifier;
		if (dir == 1) 
			directionalModifier = Vector3.forward * centerOffset;
		else if (dir == 2)
			directionalModifier = Vector3.forward * -centerOffset;
		else if (dir == 3)
			directionalModifier = Vector3.right * -centerOffset;
		else if (dir == 4)
			directionalModifier = Vector3.right * centerOffset;
		else
			directionalModifier = Vector3.zero;
	
		Debug.DrawRay (transform.position + directionalModifier, Vector3.down * groundCheckDist, Color.red);

		if (Physics.Raycast (transform.position + directionalModifier, Vector3.down, out hit, groundCheckDist, groundMask)) {
			print (hit.transform);
			result = true;
			focusScript.UpdateStand(hit.point);
		} else {
			result = false;
		}
		return result;
	}

	void ExtraGravity(){
		if (!grounded)
			rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y - gravityMultiplier, rb.velocity.z);
	}

	void GroundCheck(){
		bool groundN, groundS, groundE, groundW;
		groundN = CheckRay (1);
		groundS = CheckRay (2);
		groundE = CheckRay (3);
		groundW = CheckRay (4);

		if (groundN || groundS || groundE || groundW) {
			grounded = true;
		} else {
			grounded = false;
		}
	}

	void ApplyMovement(){
		Vector3 movementVector = new Vector3 (h, 0, v);
		//movementVector = Vector3.Normalize (movementVector);
//print ("movement vector is " + movementVector);
		movementVector *= walkSpeed;
		rb.velocity = new Vector3 (movementVector.x, rb.velocity.y, movementVector.z);
	}

	void Jump(){
		if (grounded) 
			rb.velocity = new Vector3 (rb.velocity.x, jumpSpeed, rb.velocity.z);
	}

	void StopJump(){
		if (rb.velocity.y > jumpStopSpeed)
			rb.velocity = new Vector3(rb.velocity.x, jumpStopSpeed, rb.velocity.z);
	}
}
