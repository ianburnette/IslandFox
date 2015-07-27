using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class levelManager : MonoBehaviour {

	public Image fadeImage;
	public float fadeTime, fadeIncrement;
	private float targetAlpha;
	public GameObject loadingText;
	bool changeLevel;
	int levelToLoad;

	void OnLevelWasLoaded(){

	}

	void Unfade(){
		targetAlpha = 0;
	}

	public void ChangeLevel(int newLevel){
		levelToLoad = newLevel;
		changeLevel = true;
		FadeOut ();
	}

	void FadeOut(){
		targetAlpha = 1f;
	}

	// Use this for initialization
	void Start () {
		fadeImage.enabled = true;
		Unfade ();
	}
	
	// Update is called once per frame
	void Update () {
		if (fadeImage.color.a > targetAlpha) {
			fadeImage.color = new Color (fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a - (fadeIncrement * fadeTime));
		}else if (fadeImage.color.a < targetAlpha) {
			fadeImage.color = new Color (fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a + (fadeIncrement * fadeTime));
		}
		if (changeLevel && fadeImage.color.a >= targetAlpha) {
			loadingText.SetActive(true);
			changeLevel = false;
			Application.LoadLevel(levelToLoad);
		}
	}
}
