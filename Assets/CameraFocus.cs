using UnityEngine;
using System.Collections;

public class CameraFocus : MonoBehaviour {

	public Transform player;
	public Vector3 heightFocus;
	public float heightMargin;
	public float moveTime, verticalMoveTime;

	public float cameraHeight, cameraDist;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp (transform.position, 
		                                   new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z + cameraDist),
		                                   moveTime * Time.deltaTime);
		transform.position = Vector3.Lerp (transform.position, 
		                                   new Vector3(transform.position.x, player.transform.position.y + cameraHeight, transform.position.z),
		                                   verticalMoveTime * Time.deltaTime);
	}

	public void UpdateStand(Vector3 standPoint){
		if (standPoint.y > (heightFocus.y + heightMargin)) {
			heightFocus = standPoint;
		} else if (standPoint.y < (heightFocus.y - heightMargin)) {
			heightFocus = standPoint;
		}
	}
}
