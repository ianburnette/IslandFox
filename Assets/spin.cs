using UnityEngine;
using System.Collections;

public class spin : MonoBehaviour {

	public float spinSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate (Vector3.up * spinSpeed * Time.deltaTime);
	}
}
