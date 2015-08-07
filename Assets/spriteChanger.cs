using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class spriteChanger : MonoBehaviour {

	public Color nightSpriteColor;

	// Use this for initialization
	void OnEnable () {
		SpriteRenderer[] sprites = FindObjectsOfType(typeof(SpriteRenderer)) as SpriteRenderer[];
		foreach (SpriteRenderer sprt in sprites) {
			sprt.color = nightSpriteColor;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
