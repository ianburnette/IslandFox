using UnityEngine;
using System.Collections;

public class vineContainer : MonoBehaviour {

	public Transform[] vineTransforms;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void MakeParent(){
		foreach (Transform vine in vineTransforms) {
			vine.parent = transform;
		}
	}
}
