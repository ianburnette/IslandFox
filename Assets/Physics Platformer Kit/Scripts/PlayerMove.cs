﻿using UnityEngine;
using System.Collections;

//handles player movement, utilising the CharacterMotor class
[RequireComponent(typeof(CharacterMotor))]
[RequireComponent(typeof(DealDamage))]
[RequireComponent(typeof(AudioSource))]
public class PlayerMove : MonoBehaviour 
{
	//custom
	public Transform myCamFocus;
	public cameraFocusControls focusControls;
	public Transform mainCam;
	public float additionalGravityForce;
	public float jumpStopSpeed;
	public Animator anim;
	public cameraChanger camChange;
	public bool haveCameraFocus = true;
	private Vector3 camFocusLocation;
	public GameObject smokePuff;
	public float debugh, debugv;
	public Vector3 hitPoint;
	public float hitOffset;

	public PlayerInventory inventory;
	public playerIslandGrow islandGrower;
	public Transform standingOnTransform;

	//setup
	public bool sidescroller;					//if true, won't apply vertical input
	public Transform floorChecks;		//main camera, and floorChecks object. FloorChecks are raycasted down from to check the player is grounded.
	public Animator animator;					//object with animation controller on, which you want to animate
	public AudioClip jumpSound;					//play when jumping
	public AudioClip landSound;					//play when landing on ground
	
	//movement
	public float accel = 70f;					//acceleration/deceleration in air or on the ground
	public float airAccel = 18f;			
	public float decel = 7.6f;
	public float airDecel = 1.1f;
	[Range(0f, 5f)]
	public float rotateSpeed = 0.7f, airRotateSpeed = 0.4f;	//how fast to rotate on the ground, how fast to rotate in the air
	public float maxSpeed = 9;								//maximum speed of movement in X/Z axis
	public float slopeLimit = 40, slideAmount = 35;			//maximum angle of slopes you can walk on, how fast to slide down slopes you can't
	public float movingPlatformFriction = 7.7f;				//you'll need to tweak this to get the player to stay on moving platforms properly
	
	//jumping
	public Vector3 jumpForce =  new Vector3(0, 13, 0);		//normal jump force
	public Vector3 secondJumpForce = new Vector3(0, 13, 0); //the force of a 2nd consecutive jump
	public Vector3 thirdJumpForce = new Vector3(0, 13, 0);	//the force of a 3rd consecutive jump
	public float jumpDelay = 0.1f;							//how fast you need to jump after hitting the ground, to do the next type of jump
	public float jumpLeniancy = 0.17f;						//how early before hitting the ground you can press jump, and still have it work
	[HideInInspector]
	public int onEnemyBounce;					
	
	private int onJump;
	private bool grounded;
	private Transform[] floorCheckers;
	private Quaternion screenMovementSpace;
	private float airPressTime, groundedCount, curAccel, curDecel, curRotateSpeed, slope;
	private Vector3 direction, moveDirection, screenMovementForward, screenMovementRight, movingObjSpeed;
	
	private CharacterMotor2 characterMotor;
	private EnemyAI enemyAI;
	private DealDamage dealDamage;

	private Rigidbody rb;
	//setup
	void Awake()
	{
		myCamFocus = GameObject.Find ("Camera Focus").transform;
		focusControls = myCamFocus.GetComponent<cameraFocusControls> ();
		camFocusLocation = myCamFocus.position;
		inventory = GetComponent<PlayerInventory> ();
		if (GetComponent<playerIslandGrow> () != null) {
			islandGrower = GetComponent<playerIslandGrow> ();
		}
		camChange = GetComponent<cameraChanger> ();
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody> ();
		//create single floorcheck in centre of object, if none are assigned
		if(!floorChecks)
		{
			floorChecks = new GameObject().transform;
			floorChecks.name = "FloorChecks";
			floorChecks.parent = transform;
			floorChecks.position = transform.position;
			GameObject check = new GameObject();
			check.name = "Check1";
			check.transform.parent = floorChecks;
			check.transform.position = transform.position;
			Debug.LogWarning("No 'floorChecks' assigned to PlayerMove script, so a single floorcheck has been created", floorChecks);
		}
		//assign player tag if not already
		if(tag != "Player")
		{
			tag = "Player";
			Debug.LogWarning ("PlayerMove script assigned to object without the tag 'Player', tag has been assigned automatically", transform);
		}
		//usual setup
		mainCam = GameObject.FindGameObjectWithTag("MainCamera").transform;
		dealDamage = GetComponent<DealDamage>();
		characterMotor = GetComponent<CharacterMotor2>();
		//gets child objects of floorcheckers, and puts them in an array
		//later these are used to raycast downward and see if we are on the ground
		floorCheckers = new Transform[floorChecks.childCount];
		for (int i=0; i < floorCheckers.Length; i++)
			floorCheckers[i] = floorChecks.GetChild(i);
	}
	
	//get state of player, values and input
	void Update()
	//void UpdateReplacement()
	{	
		//handle jumping
//		JumpCalculations ();
//		CheckStandingTransform ();
		//adjust movement values if we're in the air or on the ground
		curAccel = (grounded) ? accel : airAccel;
		curDecel = (grounded) ? decel : airDecel;
		curRotateSpeed = (grounded) ? rotateSpeed : airRotateSpeed;
				
		//get movement axis relative to camera
		screenMovementSpace = Quaternion.Euler (0, mainCam.eulerAngles.y, 0);
		screenMovementForward = screenMovementSpace * Vector3.forward;
		screenMovementRight = screenMovementSpace * Vector3.right;
		
		//get movement input, set direction to move in
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

//		float h = Input.GetAxis ("Horizontal");
//		float v = Input.GetAxis ("Vertical");

		debugh = h;
		debugv = v;
		//only apply vertical input to movemement, if player is not sidescroller
		if(!sidescroller)
			direction = (screenMovementForward * v) + (screenMovementRight * h);
		else
			direction = Vector3.right * h;
//		print ("direction is " + direction);
//		print ("normalized direction is " + direction.normalized);
		moveDirection = transform.position + direction.normalized;
		Animate (moveDirection, h, v);
	}

	void CheckStandingTransform(){
	//	print (standingOnTransform.name);
		if (grounded && standingOnTransform!=null){
			if (standingOnTransform.tag == "Ground"){
				inventory.canPlace = true;
				if (islandGrower != null)
				islandGrower.canPlace = true;
			}else{
				inventory.canPlace = false;
				if (islandGrower != null)
					islandGrower.canPlace = false;
			}
		}else{
			inventory.canPlace = false;
			if (islandGrower != null)
				islandGrower.canPlace = false;
		}
	}
	
	void Animate(Vector3 moveDir, float h, float v){
		anim.SetBool ("grounded", grounded);
		anim.SetFloat ("xDir", h);
		anim.SetFloat ("zDir", v);

		if (h * accel != 0 || v * accel != 0) {
			anim.SetBool ("moving", true);
		} else if (h * accel ==0 && v * accel ==0){
			anim.SetBool ("moving", false);
		}

		if (Mathf.Abs (h) >= Mathf.Abs (v)) { //moving right
			if (h > 0) {
				if (anim.GetBool("movingE") != true){
					anim.SetBool ("movingE", true);
					anim.SetBool ("movingW", false);
					anim.SetBool ("movingS", false);
					anim.SetBool ("movingN", false);
				}
			} else if (h < 0) {
				if (anim.GetBool("movingW") != true){
					anim.SetBool ("movingW", true);
					anim.SetBool ("movingE", false);
					anim.SetBool ("movingS", false);
					anim.SetBool ("movingN", false);
				}
			}
		} else if (Mathf.Abs (v) > Mathf.Abs (h)) {
			if (v>0){
				if (anim.GetBool("movingN") != true){
					anim.SetBool ("movingN", true);
					anim.SetBool ("movingE", false);
					anim.SetBool ("movingS", false);
					anim.SetBool ("movingW", false);
				}
			}else if (v<0){
				if (anim.GetBool("movingS") != true){
					anim.SetBool ("movingS", true);
					anim.SetBool ("movingE", false);
					anim.SetBool ("movingN", false);
					anim.SetBool ("movingW", false);
				}
			}

		}

//		if (Mathf.Abs (moveDir.x) > Mathf.Abs (moveDir.z)) {
//			if (moveDir.x > 0) {
//				anim.SetBool ("movingE", true);
//				anim.SetBool ("movingW", false);
//				anim.SetBool ("movingS", false);
//				anim.SetBool ("movingN", false);
//			} else if (moveDir.x < 0) {
//				anim.SetBool ("movingW", true);
//				anim.SetBool ("movingE", false);
//				anim.SetBool ("movingS", false);
//				anim.SetBool ("movingN", false);
//			}
//		} else if (Mathf.Abs (moveDir.x) < Mathf.Abs (moveDir.z)) {
//			if (moveDir.z > 0) {
//				anim.SetBool ("movingN", true);
//				anim.SetBool ("movingW", false);
//				anim.SetBool ("movingS", false);
//				anim.SetBool ("movingE", false);
//			} else if (moveDir.z < 0) {
//				anim.SetBool ("movingS", true);
//				anim.SetBool ("movingE", false);
//				anim.SetBool ("movingW", false);
//				anim.SetBool ("movingN", false);
//			}
//		} else {
//			anim.SetBool ("movingS", false);
//			anim.SetBool ("movingE", false);
//			anim.SetBool ("movingW", false);
//			anim.SetBool ("movingN", false);
//		}
	}

	//apply correct player movement (fixedUpdate for physics calculations)
	void FixedUpdate() 
	{
		JumpCalculations ();
		CheckStandingTransform ();
		//UpdateReplacement ();
		//are we grounded
		grounded = IsGrounded ();
		//move, rotate, manage speed
		characterMotor.MoveTo (moveDirection, curAccel, 0.9f, true);
		if (rotateSpeed != 0 && direction.magnitude != 0)
			characterMotor.RotateToDirection (moveDirection , curRotateSpeed * 5, true);
		characterMotor.ManageSpeed (curDecel, maxSpeed + movingObjSpeed.magnitude, true);
		//set animation values
		if(animator)
		{
			animator.SetFloat("DistanceToTarget", characterMotor.DistanceToTarget);
			animator.SetBool("Grounded", grounded);
			animator.SetFloat("YVelocity", GetComponent<Rigidbody>().velocity.y);
		}
	}
	
	//prevents rigidbody from sliding down slight slopes (read notes in characterMotor class for more info on friction)
	void OnCollisionStay(Collision other)
	{
		//only stop movement on slight slopes if we aren't being touched by anything else
		if (other.collider.tag != "Untagged" || grounded == false)
			return;
		//if no movement should be happening, stop player moving in Z/X axis
		if(direction.magnitude == 0 && slope < slopeLimit && GetComponent<Rigidbody>().velocity.magnitude < 2)
		{
			//it's usually not a good idea to alter a rigidbodies velocity every frame
			//but this is the cleanest way i could think of, and we have a lot of checks beforehand, so it shou
			GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
	}
	
	//returns whether we are on the ground or not
	//also: bouncing on enemies, keeping player on moving platforms and slope checking
	private bool IsGrounded() 
	{
		//get distance to ground, from centre of collider (where floorcheckers should be)
		float dist = GetComponent<Collider>().bounds.extents.y;
		//check whats at players feet, at each floorcheckers position
		foreach (Transform check in floorCheckers)
		{
			RaycastHit hit;
			if(Physics.Raycast(check.position, Vector3.down, out hit, dist + 0.05f))
			{
				//print (hit.transform.tag);
				if(!hit.transform.GetComponent<Collider>().isTrigger && hit.transform.tag != "Checkpoint"&& hit.transform.tag != "NPC")
				{
					//slope control
					slope = Vector3.Angle (hit.normal, Vector3.up);
					//slide down slopes
					if(slope > slopeLimit && hit.transform.tag != "Pushable")
					{
						Vector3 slide = new Vector3(0f, -slideAmount, 0f);
						GetComponent<Rigidbody>().AddForce (slide, ForceMode.Force);
					}
					//enemy bouncing
					if (hit.transform.tag == "Enemy" && GetComponent<Rigidbody>().velocity.y < 0)
					{
						enemyAI = hit.transform.GetComponent<EnemyAI>();
						enemyAI.BouncedOn();
						onEnemyBounce ++;
						dealDamage.Attack(hit.transform.gameObject, 1, 0f, 0f);
					}
					else
						onEnemyBounce = 0;
					//moving platforms
					if (hit.transform.tag == "Pushable" || hit.transform.tag == "Pushable")
					{
						movingObjSpeed = hit.transform.GetComponent<Rigidbody>().velocity;
						print ("moving object speed is " + movingObjSpeed);
						movingObjSpeed.y = 0f;
						//9.5f is a magic number, if youre not moving properly on platforms, experiment with this number
						GetComponent<Rigidbody>().AddForce(movingObjSpeed * movingPlatformFriction * Time.fixedDeltaTime, ForceMode.VelocityChange);
					}
					else
					{
						movingObjSpeed = Vector3.zero;
					}
					if (hit.transform.tag == "LargeFlower" && haveCameraFocus){
						camChange.Change(hit.transform.root.GetChild (0).position, false);
						haveCameraFocus = false;
					}
					if (!haveCameraFocus && hit.transform.tag != "LargeFlower"){
						camChange.Change (camFocusLocation, true);
						haveCameraFocus = true;
						print ("switching back");
					}
					if (hit.transform.tag != "Resetter"){
						focusControls.StandingOn(hit.point.y);
					}
					hitPoint = hit.normal;
					//transform.position = new Vector3(transform.position.x, hitPoint.y + hitOffset, transform.position.z);
					if (standingOnTransform != hit.transform){
						standingOnTransform = hit.transform;
					}
					//yes our feet are on something
					return true;
				}
			}
		}
		movingObjSpeed = Vector3.zero;
		//no none of the floorchecks hit anything, we must be in the air (or water)
		return false;
	}
	
	//jumping
	private void JumpCalculations()
	{
		Vector3 vel = GetComponent<Rigidbody> ().velocity;
		//keep how long we have been on the ground
		groundedCount = (grounded) ? groundedCount += Time.deltaTime : 0f;
		
		//play landing sound
		if(groundedCount < 0.25 && groundedCount != 0 && !GetComponent<AudioSource>().isPlaying && landSound && GetComponent<Rigidbody>().velocity.y < 1)
		{
			//GetComponent<AudioSource>().volume = Mathf.Abs(GetComponent<Rigidbody>().velocity.y)/40;
			GetComponent<AudioSource>().clip = landSound;
			GetComponent<AudioSource>().Play ();
			Puff();
		}
		//if we press jump in the air, save the time
		if (Input.GetButtonDown ("A") && !grounded) {
			airPressTime = Time.time;
		}

		if (!grounded) {
			rb.AddForce(Vector3.down * additionalGravityForce);
		}

		if (Input.GetButtonUp ("A") && !grounded && vel.y > 1f) {
			GetComponent<Rigidbody>().velocity = new Vector3(vel.x, jumpStopSpeed, vel.z);
		}
		
		//if were on ground within slope limit
		if (grounded && slope < slopeLimit)
		{
			//and we press jump, or we pressed jump justt before hitting the ground
			if (Input.GetButtonDown ("A") || airPressTime + jumpLeniancy > Time.time)
			{	
				//increment our jump type if we haven't been on the ground for long
				onJump = (groundedCount < jumpDelay) ? Mathf.Min(2, onJump + 1) : 0;
				//execute the correct jump (like in mario64, jumping 3 times quickly will do higher jumps)
				if (onJump == 0)
						Jump (jumpForce);
				else if (onJump == 1)
						Jump (secondJumpForce);
				else if (onJump == 2)
						Jump (thirdJumpForce);
			}
		}
	}

	void Puff(){
		Instantiate (smokePuff, transform.position - (Vector3.up * 0.5f), Quaternion.identity);
	}

	//push player at jump force
	public void Jump(Vector3 jumpVelocity)
	{
		if(jumpSound)
		{
			GetComponent<AudioSource>().volume = 1;
			GetComponent<AudioSource>().clip = jumpSound;
			GetComponent<AudioSource>().Play ();
		}
		Puff();
		GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 0f, GetComponent<Rigidbody>().velocity.z);
		GetComponent<Rigidbody>().AddRelativeForce (jumpVelocity, ForceMode.Impulse);
		airPressTime = 0f;
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere (hitPoint, .3f);
	}
}