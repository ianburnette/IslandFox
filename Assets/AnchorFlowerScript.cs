using UnityEngine;
using System.Collections;

public class AnchorFlowerScript : MonoBehaviour {

	public bool draw;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (draw) {
			Debug.DrawRay(transform.position + Vector3.up + Vector3.right, Vector3.down);
			Debug.DrawRay(transform.position + Vector3.up + Vector3.left, Vector3.down);
			Debug.DrawRay(transform.position + Vector3.up + Vector3.up, Vector3.down);
			Debug.DrawRay(transform.position + Vector3.up + Vector3.down, Vector3.down);

			Debug.DrawRay(transform.position + Vector3.up + Vector3.right + Vector3.up, Vector3.down);
			Debug.DrawRay(transform.position + Vector3.up + Vector3.left + Vector3.up, Vector3.down);
			Debug.DrawRay(transform.position + Vector3.up + Vector3.up + Vector3.right, Vector3.down);
			Debug.DrawRay(transform.position + Vector3.up + Vector3.down + Vector3.right, Vector3.down);
		}
	}

	public void Detach(){
		draw = true;
		transform.parent.gameObject.AddComponent <Rigidbody>();//GetComponent<Rigidbody> ().isKinematic = false;
		foreach (Transform child in transform.parent) {
			if (child.tag != "UI"){
				child.GetComponent<Collider>().isTrigger = true;
			}
		}



	}
}
