using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class fadeLogo : MonoBehaviour {

	public Image background, logo;

	public float fadeAfter, fadeSpeed;

	bool fading, sent;

	public mainMenuGM mainMen;

	// Use this for initialization
	void Start () {
		Invoke ("startFading", fadeAfter);
	}
	
	// Update is called once per frame
	void Update () {
		if (fading) {
			background.color = new Color (background.color.r, background.color.g, background.color.b, background.color.a - fadeSpeed);
			logo.color = new Color (logo.color.r, logo.color.g, logo.color.b, logo.color.a - fadeSpeed);
		}if (background.color.a <= .01f) {
			mainMen.logo = false;
		}
	}

	public 	void startFading(){
		fading = true;
	}
}
