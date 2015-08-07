using UnityEngine;
using System.Collections;

public class boatControls : MonoBehaviour {

	public Animator anim;
	public Transform hull, sprite;
	public float speed, strokeForce, deadZone, maxVelocity, launchVelocity, turnSpeed, maxDifference;
	private float baseSpeed;
	public float h, v;
	public Rigidbody rb;
	public Transform spriteLocation;
	public float targetHeight, heightCorrectSpeed, targetTargetHeight, bobTime;
	public LayerMask mask;
	public float frontHeight, backHeight;
	public GameObject puff;

	// Use this for initialization
	void Start () {
		puff = Resources.Load ("Smoke Puff") as GameObject;
		baseSpeed = speed;
		rb = GetComponent<Rigidbody> ();
		sprite.parent = null;
		//anim = GetComponent<Animator> ();
	}

	void OnEnable(){
		rb.isKinematic = false;
	//	InvokeRepeating ("Bob", bobTime, bobTime);
	}

	void OnDisable(){
		rb.isKinematic = true;
	}

	void Bob(){
		rb.AddForce (Vector3.up * strokeForce/3);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		GetInput ();
		GetStroke ();
		TargetHeight ();
		MoveDirection ();
		UpdateAnimator ();
		LimitSpeed ();
		CheckBelow ();
	}

	void CheckBelow(){
		RaycastHit hit;
		Debug.DrawRay (transform.position - (transform.up * 1.5f) + (transform.right/2) + (transform.forward), Vector3.down * 50f);
		Debug.DrawRay (transform.position + (transform.up * 1.5f) + (transform.right/2) + (transform.forward), Vector3.down * 50f);

		//float frontHeight, backHeight;

		bool hittingFront, hittingBack;

		if (Physics.Raycast (transform.position - (transform.up * 1.5f) + (transform.right / 2) + (transform.forward), Vector3.down, out hit, 50f, mask)) {
			//print ("boat hitting " + hit.transform);
			hittingFront = true;
			frontHeight = hit.point.y;
		} else {
			hittingFront = false;
		}if (Physics.Raycast (transform.position + (transform.up * 1.5f) + (transform.right / 2) + (transform.forward), Vector3.down, out hit, 50f, mask)) {
			//print ("boat hitting " + hit.transform);
			hittingBack = true;
			backHeight = hit.point.y;
		} else {
			hittingBack = false;
		}

		if (hittingFront == false && hittingBack == false) {
			targetHeight = targetTargetHeight;
		} else if (hittingFront && !hittingBack) {
			targetHeight = frontHeight + 2f;
		} else if (!hittingFront && hittingBack) {
			targetHeight = backHeight + 2f;
		} else {
			if (backHeight>frontHeight){
				targetHeight = backHeight + 2f;
			}else{
				targetHeight = frontHeight + 2f;
			}
		}

//
//			if (frontHeight < targetHeight && backHeight < targetHeight){
//				targetHeight = frontHeight  + 2f;
//			}else if (frontHeight > targetHeight && frontHeight < transform.position.y && frontHeight > targetHeight && frontHeight < transform.position.y){
//				targetHeight = frontHeight + 2f;
//			}
//		}else{
//			targetHeight = 10f;	
//		}
	}

	void TargetHeight(){
		float dif = transform.position.y - targetHeight;
		if (dif < 0) {
			//if (dif > -maxDifference){
				rb.AddForce (Vector3.up * -dif * heightCorrectSpeed);
		//	}else{
			//	rb.AddForce (Vector3.up * -maxDifference * heightCorrectSpeed);
			//}
		} else {
		//	if (dif < maxDifference){
				rb.AddForce (Vector3.up * -dif * heightCorrectSpeed);
			//}else{
			//	rb.AddForce (Vector3.up * -maxDifference * heightCorrectSpeed);
			//}
		}

		if (targetHeight > targetTargetHeight) {
			if (transform.position.y < targetHeight){
			//	targetHeight = transform.position.y;
			}
		}
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
		if (Input.GetButtonDown ("X")) {
			Stroke();
		}
	}

	void LaunchStroke(){
		rb.AddForce (Vector3.up * launchVelocity);
	}

	void Stroke(){
		rb.AddForce (Vector3.up * strokeForce);
		Instantiate (puff, transform.position - Vector3.up, Quaternion.identity);
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
