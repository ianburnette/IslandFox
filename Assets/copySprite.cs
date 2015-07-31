using UnityEngine;
using System.Collections;

public class copySprite : MonoBehaviour {

	public SpriteRenderer spriteToCopy, thisSprite;

	// Use this for initialization
	void Start () {
		thisSprite = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		thisSprite.sprite = spriteToCopy.sprite;
	}
}
