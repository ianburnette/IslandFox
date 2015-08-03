using UnityEngine;
using System.Collections;

public class showShadow : MonoBehaviour {

	public Transform player;
	Camera cam;
	public LayerMask mask;
	public float mult;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").transform;
		cam = GetComponent<Camera> ();
		cam.enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Debug.DrawRay (transform.position, player.transform.position - transform.position + (Vector3.up * mult));
		CheckIfBlocked ();
	}

	void CheckIfBlocked(){
		RaycastHit hit;
		if (Physics.Raycast (transform.position, player.transform.position - transform.position + (Vector3.up * mult), out hit, 100f, mask)) {
			print (hit.transform);
			if (hit.transform.tag != "Player" && cam.enabled == false){
				cam.enabled = true;
			}else if ( hit.transform.tag == "Player" && cam.enabled == true){
				cam.enabled = false;
			}
		}
	}
}
