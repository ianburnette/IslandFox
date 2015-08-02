using UnityEngine;
using System.Collections;

public class selectSprite : MonoBehaviour {

	public Sprite[] seedSprites;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetSprite(int index){
		transform.GetChild(0).GetComponent<SpriteRenderer> ().sprite = seedSprites [index];

	}
}
