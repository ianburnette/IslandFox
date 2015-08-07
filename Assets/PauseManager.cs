using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems; 
using UnityEngine.UI;


public class PauseManager : MonoBehaviour {

	public bool paused = false;
	public bool pauseScreen = false;
	public bool inventory = false;

	public EventSystem eventMan;

	public GameObject pauseUI, inventoryUI;
	public GameObject resumeButton;

	persistentAudio audioMan;

	public Slider musicSlider, masterSlider;

	public GameObject pauseScreenPanel, controlsScreen;

	public GameObject controlsGoBack;

	// Use this for initialization
	void Start () {
		//eventMan = GameObject.Find ("EventSystem").GetComponent<EventSystem> ();// as EventSystem;
		//resumeButton = transform.GetChild (2).GetChild (3).GetChild (2).GetChild (2).gameObject;
		audioMan = GameObject.Find ("persistentAudioGM").GetComponent<persistentAudio> ();
		GetSliders ();
		SetVolumes ();
	}

	void OnLevelWasLoaded(int level){
		audioMan = GameObject.Find ("persistentAudioGM").GetComponent<persistentAudio> ();
	}

	void GetSliders(){
		//musicSlider = transform.GetChild (2).GetChild (3).GetChild (2).GetChild (3).gameObject.GetComponent<Slider>();
		//masterSlider = transform.GetChild (2).GetChild (3).GetChild (2).GetChild (4).gameObject.GetComponent<Slider>();
	}

	public void ReloadCheckpoint(){
		TogglePause ();
		PauseScreen (false);
		GameObject.Find ("TheDrink").GetComponent<Resetter> ().Reset ();
	}
	
	public void SaveAndQuit(){
		
		TogglePause ();
		PauseScreen (false);
		GameObject.Find ("persistentGM").GetComponent<persistentInventory> ().SaveInventory ();
		audioMan.SaveLevels ();
		PlayerPrefs.SetInt ("SaveLevel", Application.loadedLevel);
		//ToggleInventory (false);
		GetComponent<levelManager> ().ChangeLevel (0);
	}

	public void PauseButtonPressed(){
		if (paused && !inventory){
			TogglePause();
			PauseScreen(false);
		}else if (!paused && !inventory){
			TogglePause();
			PauseScreen(true);
		}else if (paused && inventory){
			ToggleInventory(false);
		}
	}

	
	public void DecAud(int which){
		if (which == 0) {
			musicSlider.value-=0.1f;
		}if (which == 1) {
			masterSlider.value-=0.1f;
		}
	}
	
	public void IncAud(int which){
		if (which == 0) {
			musicSlider.value+=0.1f;
		}if (which == 1) {
			masterSlider.value+=0.1f;
		}
	}

	void SetVolumes(){
		musicSlider.value = audioMan.musicLevel;
		masterSlider.value = audioMan.masterVolume;
	}

	void UpdateVolumes(){
		audioMan.musicLevel = musicSlider.value;// = audioMan.musicLevel;
		audioMan.masterVolume = masterSlider.value;
	}

	public void ViewControls(){
		pauseScreenPanel.SetActive (false);
		controlsScreen.SetActive (true);
		eventMan.SetSelectedGameObject (controlsGoBack, new BaseEventData(eventMan));
	}

	public void HideControls(){
		pauseScreenPanel.SetActive (true);
		controlsScreen.SetActive (false);
		eventMan.SetSelectedGameObject (resumeButton, new BaseEventData(eventMan));
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown("Pause")){
			if (paused && !inventory){
				TogglePause();
				PauseScreen(false);
			}else if (!paused && !inventory){
				TogglePause();
				eventMan.SetSelectedGameObject (resumeButton ,new BaseEventData(eventMan));
				PauseScreen(true);
			}else if (paused && inventory){
				ToggleInventory(false);
				PauseScreen(true);
				eventMan.SetSelectedGameObject (resumeButton ,new BaseEventData(eventMan));
			}
		}
		if (Input.GetButtonDown ("Inventory")) {
			//print ("pressing button " + paused + inventory);
			if (!paused ){
			//	print ("not paused");
				TogglePause();
				ToggleInventory(true);
			}
			else if (paused && !inventory){
				ToggleInventory(true);
				//PauseScreen(false);
			}else if (paused && inventory){
				TogglePause();
				ToggleInventory(false);
			}
		}

		if (paused) {
			UpdateVolumes ();
		}
	}

	void ToggleInventory(bool status){
		//print ("toggling ");
		if (status) {
			inventoryUI.SetActive (true);
			inventory = true;
		} else {
			inventoryUI.SetActive(false);
			inventory = false;
		}
	}

	void TogglePause(){
//		print ("pausing");
		HideControls ();

		Time.timeScale = 1 - Time.timeScale;
		paused = !paused;
		eventMan.SetSelectedGameObject (resumeButton ,new BaseEventData(eventMan));
		//ToggleInventory (false);
		//PauseScreen(paused);
	}

	void PauseScreen(bool status){
		if (status) {
			eventMan.SetSelectedGameObject (resumeButton ,new BaseEventData(eventMan));
			pauseUI.SetActive (true);
			pauseScreen = true;
		} else {
			pauseUI.SetActive (false);
			pauseScreen = false;
			audioMan.SaveLevels ();
		}

	}
}
