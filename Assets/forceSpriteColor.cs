using UnityEngine;
using System.Collections;

public class forceSpriteColor : MonoBehaviour {

	public Color rainColor;
	SpriteRenderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (rend.color != rainColor) {
			rend.color = rainColor;
		}
	}
}
