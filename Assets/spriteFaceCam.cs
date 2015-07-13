using UnityEngine;
using System.Collections;

public class spriteFaceCam : MonoBehaviour {

	public Transform cam;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentRotation = transform.rotation.eulerAngles;
		transform.LookAt (cam);
		//transform.rotation = Quaternion.Euler (transform.rotation.x, currentRotation.y, currentRotation.z);
	}
}
