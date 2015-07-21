using UnityEngine;
using System.Collections;

public class cameraFocusControls : MonoBehaviour {

	public Transform player;
	public Rigidbody playerRB, rb;
	public float horizontalMargin;
	public float horizontalOffset;
	public float grossXvel, grossZvel;
	public int facingXDirection = 0; //1=w, 2=e
	public int facingZDirection = 0; //1=n, 2=s
	public float currentXoffset, currentZoffset, yOffset;
	public float groundHeight, minHeightDist, waitToChangeTime, changeSpeed;
	public bool readyToChangeHeight;

	

	public void StandingOn(float height){
		if (readyToChangeHeight) {
			if (Mathf.Abs (height) - Mathf.Abs (groundHeight) > minHeightDist || 
				Mathf.Abs (groundHeight) - Mathf.Abs (height) > minHeightDist) {
				ChangeHeight (height);
			}
		}

	}

	void ChangeHeight(float newHeight){
		readyToChangeHeight = false;
		Invoke ("ResetHeightChange", waitToChangeTime);
		iTween.ValueTo (gameObject, iTween.Hash(
			"from", groundHeight,
			"to", newHeight,
			"time", changeSpeed,
			"onupdate", "UpdateHeight"
			));
	}

	void UpdateHeight(float newHeight){
		groundHeight = newHeight;
	}

	void ResetHeightChange(){
		readyToChangeHeight = true;
	}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		transform.position = new Vector3 (transform.position.x, player.transform.position.y, transform.position.z);
		groundHeight = player.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
//		grossXvel += playerRB.velocity.x;
//		grossZvel += playerRB.velocity.z;
//		if (grossXvel > horizontalMargin) {
//			facingXDirection = 1;
//		} else if (grossXvel < -horizontalMargin) {
//			facingXDirection = 2;
//		}
//
//		if (facingXDirection == 1 && playerRB.velocity.x > 0) {
//			rb.velocity = new Vector3(playerRB.velocity.x, rb.velocity.y, rb.velocity.z);
//		}else if (facingXDirection == 2 && playerRB.velocity.x < 0) {
//			rb.velocity = new Vector3(playerRB.velocity.x, rb.velocity.y, rb.velocity.z);
//		}

		transform.position = new Vector3 (player.transform.position.x,
		                                 groundHeight + yOffset,
		                                 player.transform.position.z);
			//player.transform.position + new Vector3 (currentXoffset, yOffset, currentZoffset);
	}

	void OnDrawGizmos(){
		Gizmos.DrawSphere(player.transform.position + new Vector3 (horizontalOffset, 0, 0), .3f);
		Gizmos.DrawSphere(player.transform.position + new Vector3 (-horizontalOffset, 0, 0), .3f);
	}
}

