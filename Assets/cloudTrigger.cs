using UnityEngine;
using System.Collections;

public class cloudTrigger : MonoBehaviour {

	public Transform boat;
	public float resetTime = 2f;
	public float centerSpeed;
	public bool onCloud;
	public SphereCollider sphereCol;
	public cloudControls cloudCont;
	public boatControls boatCont;
	public CloudWander wanderScript;

	// Use this for initialization
	void Start () {
		sphereCol = GetComponent<SphereCollider> ();
		cloudCont = GetComponent<cloudControls> ();
		wanderScript = GetComponent<CloudWander> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (onCloud && Input.GetButtonDown ("Fire1")) {
			sphereCol.enabled = false;
			onCloud = false;
			cloudCont.enabled = false;
			if (boatCont!=null){
				boatCont.OnCloud(false);
			}
			Invoke ("ResetCollider", resetTime);
			wanderScript.enabled=true;
		}
		if (onCloud) {
			boat.transform.position = Vector3.Lerp (boat.transform.position, 
			                                        new Vector3(transform.position.x, transform.position.y, transform.position.z),
			                                     	centerSpeed * Time.deltaTime);
		}
	}

	void ResetCollider(){
		sphereCol.enabled = true;
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Boat") {
			boatCont = col.GetComponent<boatControls>();
			boatCont.OnCloud(true);
			onCloud = true;
			cloudCont.enabled = true;
			boat = col.transform;
			wanderScript.enabled=false;
		}
	}
}
