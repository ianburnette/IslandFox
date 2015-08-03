using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class levelManager : MonoBehaviour {

	public bool debug;

	public Image fadeImage;
	public float fadeTime, fadeIncrement;
	private float targetAlpha;
	public GameObject loadingText;
	public bool changeLevel;
	public int levelToLoad;

	void OnLevelWasLoaded(){

	}

	void Unfade(){
		targetAlpha = 0;
	}



	public void ChangeLevel(int newLevel){
		levelToLoad = newLevel;
		changeLevel = true;
		FadeOut ();
		GameObject.Find ("persistentGM").GetComponent<persistentInventory> ().SaveInventory ();
		if (newLevel == 0) {
			Destroy (GameObject.Find ("persistentAudioGM"));
		}

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
		if (changeLevel && fadeImage.color.a >= targetAlpha - 0.1f) {
			print ("changing?");
			loadingText.SetActive(true);
			changeLevel = false;
			GameObject.Find ("persistentGM").GetComponent<persistentGMScript>().comingFrom = Application.loadedLevel;
			Application.LoadLevel(levelToLoad);
		}
		if (debug) {
			if (Input.GetKeyDown (KeyCode.Alpha1)){
				ChangeLevel(1);
			}if (Input.GetKeyDown (KeyCode.Alpha2)){
				ChangeLevel(2);
			}if (Input.GetKeyDown (KeyCode.Alpha3)){
				ChangeLevel(3);
			}if (Input.GetKeyDown (KeyCode.Alpha4)){
				ChangeLevel(4);
			}if (Input.GetKeyDown (KeyCode.Alpha5)){
				ChangeLevel(5);
			}if (Input.GetKeyDown (KeyCode.Alpha6)){
				ChangeLevel(0);
			}
		}
	}
}
