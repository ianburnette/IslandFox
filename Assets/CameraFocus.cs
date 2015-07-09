using UnityEngine;
using System.Collections;

public class CameraFocus : MonoBehaviour {

	public Transform player;
	public Vector3 heightFocus;
	public float heightMargin;

	public float cameraHeight, cameraDist;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(player.transform.position.x, heightFocus.y + cameraHeight, player.transform.position.z + cameraDist);
	}

	public void UpdateStand(Vector3 standPoint){
		if (standPoint.y > (heightFocus.y + heightMargin)) {
			heightFocus = standPoint;
		} else if (standPoint.y < (heightFocus.y - heightMargin)) {
			heightFocus = standPoint;
		}
	}
}
