using UnityEngine;
using System.Collections;

public class persistentAudio : MonoBehaviour {

	public float musicLevel;
	public float masterVolume;
	public float musicMult = 2f;
	public float currentMult;
	//public AudioListener camListener;

	public bool muted;

	public float fadeTime;

	public AudioClip mainMenu, level1, level2, level3, level4A, level4B, level5A, level5B, level5C, targetClip;

	public AudioClip currentClip;

	public AudioSource musicSource;

	// Use this for initialization
	void Start () {
		//if (GameObject.FindGameObjectsWithTag

		if (PlayerPrefs.GetFloat ("musicLevel") != 0) {
			musicLevel = PlayerPrefs.GetFloat ("musicLevel");
		}if (PlayerPrefs.GetFloat ("masterVolume") != 0) {
			masterVolume = PlayerPrefs.GetFloat ("masterVolume");
		}



		GameObject.DontDestroyOnLoad (gameObject);
		musicSource = GetComponent<AudioSource> ();
		currentClip = mainMenu;
		currentMult = musicMult;
	}

	public void SaveLevels(){
		PlayerPrefs.SetFloat ("musicLevel", musicLevel);
		PlayerPrefs.SetFloat ("masterVolume", masterVolume);

	}

	void OnLevelWasLoaded(int level){
		if (level == 0) {
			targetClip = mainMenu;
		}if (level == 1) {
			targetClip = level1;
		}
		if (level == 2) {
			targetClip = level2;
		}if (level == 3) {
			targetClip = level3;
		}if (level == 4) {
			targetClip = level4A;
		}if (level == 5) {
			targetClip = level5A;
		}
	}

	public void ToggleMute(bool which){
		print ("toggled");
		muted = which;
	}

	void ChangeSong(int songToChange){
		if (songToChange == 6) {
			targetClip = level5B;
		} else if (songToChange == 7) {
			targetClip = level5C;
		}
	}
	
	// Update is called once per frame
	void Update () {
		musicSource.volume = musicLevel * currentMult;
		AudioListener.volume = masterVolume;
		if (currentClip != targetClip) {
			currentMult -= fadeTime * Time.deltaTime;
			if (currentMult <= 0) {

				currentClip = targetClip;
				musicSource.clip = currentClip;
				musicSource.Play ();
			}
		} else if (currentMult < musicMult && !muted) {
			//print ("increasing");
			currentMult += fadeTime * Time.deltaTime;
		}

		if (muted && currentMult>0) {
			currentMult -= fadeTime * Time.deltaTime;
		}
	}
}
