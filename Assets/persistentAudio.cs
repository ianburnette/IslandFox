using UnityEngine;
using System.Collections;

public class persistentAudio : MonoBehaviour {

	public float musicLevel;
	public float masterVolume;
	public float musicMult = 2f;
	public float currentMult;
	//public AudioListener camListener;

	public float fadeTime;

	public AudioClip mainClip, homeIsland, targetClip;

	public AudioClip currentClip;

	public AudioSource musicSource;

	// Use this for initialization
	void Start () {
		//if (GameObject.FindGameObjectsWithTag
		musicLevel = PlayerPrefs.GetFloat ("musicLevel");
		masterVolume = PlayerPrefs.GetFloat ("masterVolume");
		GameObject.DontDestroyOnLoad (gameObject);
		musicSource = GetComponent<AudioSource> ();
		currentClip = mainClip;
		currentMult = musicMult;
	}

	public void SaveLevels(){
		PlayerPrefs.SetFloat ("musicLevel", musicLevel);
		PlayerPrefs.SetFloat ("masterVolume", masterVolume);

	}

	void OnLevelWasLoaded(int level){
		if (level == 2) {
			targetClip = homeIsland;
		}if (level == 3) {
			targetClip = mainClip;
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
		} else if (currentMult < musicMult) {
			//print ("increasing");
			currentMult += fadeTime * Time.deltaTime;
		}
	}
}
