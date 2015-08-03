using UnityEngine;
using System.Collections;

public class persistentAudio : MonoBehaviour {

	public float musicLevel;
	public float masterVolume;
	public float musicMult = 2f;
	//public AudioListener camListener;

	public AudioSource musicSource;

	// Use this for initialization
	void Start () {
		//if (GameObject.FindGameObjectsWithTag
		musicLevel = PlayerPrefs.GetFloat ("musicLevel");
		masterVolume = PlayerPrefs.GetFloat ("masterVolume");
		GameObject.DontDestroyOnLoad (gameObject);
		musicSource = GetComponent<AudioSource> ();
	}

	public void SaveLevels(){
		PlayerPrefs.SetFloat ("musicLevel", musicLevel);
		PlayerPrefs.SetFloat ("masterVolume", masterVolume);

	}

	void OnLevelWasLoaded(int level){
	//	camListener = Camera.main.GetComponent<AudioListener>();
	}
	
	// Update is called once per frame
	void Update () {
		musicSource.volume = musicLevel * musicMult;
		AudioListener.volume = masterVolume;
	}
}
