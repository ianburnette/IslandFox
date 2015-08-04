using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems; 
using UnityEngine.UI;

public class mainMenuGM : MonoBehaviour {

	public bool savedGame;
	public int savedLevel;

	public GameObject fadeScreen, title;

	public int screenShowing;

	public GameObject[] screens;
	public GameObject[] screenButtons;

	public EventSystem eventSystem;

	public Button resume, newgame;

	public float musicVol, sfxVol;
	public Slider musicSlider, sfxSlider;

	public persistentAudio persAud;
	public GameObject persInv;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("SaveLevel") != 0) {
			savedGame = true;
			savedLevel = PlayerPrefs.GetInt("SaveLevel");

		} else {
			savedGame = false;
		}
		persAud = GameObject.Find ("persistentAudioGM").GetComponent<persistentAudio> ();
	}

	public void DecAud(int which){
		if (which == 0) {
			musicSlider.value-=0.1f;
		}if (which == 1) {
			sfxSlider.value-=0.1f;
		}
	}

	public void IncAud(int which){
		if (which == 0) {
			musicSlider.value+=0.1f;
		}if (which == 1) {
			sfxSlider.value+=0.1f;
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("A")) {
			if (screenShowing == 0){
				ShowScreen(1);
				screenShowing = 1;
			}
		}
		UpdateSliders ();
//		print (PlayerPrefs.GetInt("SaveLevel"));
	}

	void UpdateSliders(){
		musicVol = musicSlider.value;
		sfxVol = sfxSlider.value;
		persAud.masterVolume = sfxVol;
		persAud.musicLevel = musicVol;
	}

	void LoadThisLevel (int thisLevel){
		if (savedLevel != 1) {
			GameObject newPersInv = (GameObject)GameObject.Instantiate(persInv, transform.position, Quaternion.identity);
			newPersInv.gameObject.transform.name = "persistentGM";
		}
		fadeScreen.SetActive (true);
		persAud.SaveLevels();
		Application.LoadLevel (thisLevel);
	}

	public void Resume(){
		if (savedGame) {
			persAud.SaveLevels();
			if (savedLevel != 1) {
				GameObject newPersInv = (GameObject)GameObject.Instantiate(persInv, transform.position, Quaternion.identity);
				newPersInv.gameObject.transform.name = "persistentGM";
			}
			Application.LoadLevel (savedLevel);
		}
	}

	public void NewGame(){
		if (savedGame) {
			ShowScreen (2);
		} else {
			LoadThisLevel(1);
		}
	}

	public void ReallyNewGame(){
		LoadThisLevel (1);
	}

	public void Back(int toWhere){
		ShowScreen (toWhere);
	}

	public void Credits(){
		title.SetActive (false);
		ShowScreen (4);
	}
	
	public void HideCredits(){
		title.SetActive (true);
		Back (1);
	}

	public void ShowScreen(int screenToShow){
		for (int i = 0; i<screens.Length; i++) {
//			print (i + " " + screenToShow);
			if (i == screenToShow){
				screens[i].SetActive(true);
			}else{
				screens[i].SetActive(false);
			}
		}

		if (screenToShow == 1 && !savedGame) {
			resume.interactable = false;
			screenButtons[1] = newgame.gameObject;
		}
		eventSystem.SetSelectedGameObject (screenButtons[screenToShow] ,new BaseEventData(eventSystem));
	}
}
