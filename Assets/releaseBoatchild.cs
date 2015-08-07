using UnityEngine;
using System.Collections;

public class releaseBoatchild : MonoBehaviour {

	Transform boat;

	// Use this for initialization
	void Start () {
		boat = transform.GetChild (0);
	}
	
	// Update is called once per frame
	void Update () {
		boat.position = transform.position;
	}

	public void Release(){
		boat.parent = null;
		boat.position = transform.position;
		Destroy (gameObject);
	}
}
