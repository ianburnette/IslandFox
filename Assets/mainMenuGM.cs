using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems; 
using UnityEngine.UI;

public class mainMenuGM : MonoBehaviour {

	public GameObject myEventSystem;

	public fadeLogo logoF;

	public bool savedGame;
	public int savedLevel;

	public GameObject fadeScreen, title;

	public bool logo;

	public int screenShowing;

	public GameObject[] screens;
	public GameObject[] screenButtons;

	public EventSystem eventSystem;
	public GameObject gmEventSystem;
	public levelManager levelMan;

	public Button resume, newgame;

	public float musicVol, sfxVol;
	public Slider musicSlider, sfxSlider;

	public persistentAudio persAud;
	public GameObject persInv;

	public Vector3 forceToPush;
	public float forceMult;

	public Rigidbody moveTut, pauseTut, invTut, interactTut, seedTut, cancelTut, jumpTut, camTut;
	public bool boolMove, boolPause, boolInv, boolInteract, boolSeed, boolCancel, boolJump, boolCam;

	public bool controlTut;

	// Use this for initialization
	void Start () {
		
		gmEventSystem = persInv.transform.GetChild (1).gameObject;
		if (PlayerPrefs.GetInt ("SaveLevel") != 0) {
			savedGame = true;
			savedLevel = PlayerPrefs.GetInt("SaveLevel");

		} else {
			savedGame = false;
		}
		persAud = GameObject.Find ("persistentAudioGM").GetComponent<persistentAudio> ();
		persInv = GameObject.Find ("persistentGM");
		levelMan = persInv.GetComponent<levelManager> ();
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
			if (logo){
				logoF.CancelInvoke();
				logoF.startFading();
				logo = false;
			}
			else if (screenShowing == 0){
				ShowScreen(1);
				screenShowing = 1;
			}
		}
		UpdateSliders ();
//		print (PlayerPrefs.GetInt("SaveLevel"));
		if (boolMove && boolPause && boolInv && boolInteract && boolSeed && boolCancel && boolJump && boolCam) {
			boolMove  = false;boolPause = false;boolInv = false; boolInteract = false; boolSeed = false; boolCancel = false; boolJump = false; boolCam = false;// false;
			Invoke ("BeginAfterTut", 1f);
	
		}
		if (controlTut) {
			ControlTutorial();
		}
	}

	public void Quit(){
		Application.Quit ();
	}

	void BeginAfterTut(){
		PlayerPrefs.DeleteAll ();
		print ("deleted all");
		LoadThisLevel(1);
	}

	void UpdateSliders(){
		musicVol = musicSlider.value;
		sfxVol = sfxSlider.value;
		persAud.masterVolume = sfxVol;
		persAud.musicLevel = musicVol;
	}

	void LoadThisLevel (int thisLevel){
		if (savedLevel != 1 && savedLevel != 0) {
			myEventSystem.SetActive(false);
			persInv.transform.GetChild(1).gameObject.SetActive(true);
			persInv.GetComponent<PauseManager>().enabled = true;
			persInv.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);

		}
		myEventSystem.SetActive(false);
		gmEventSystem.SetActive (true);
		//persInv.GetComponent<PauseManager>().enabled = true;
		fadeScreen.SetActive (true);
		persAud.SaveLevels();
		//Application.LoadLevel (thisLevel);
		levelMan.ChangeLevel (thisLevel);
	}

	public void Resume(){
		if (savedGame) {
			persAud.SaveLevels();
			if (savedLevel != 1 && savedLevel != 0) {
				myEventSystem.SetActive(false);
				persInv.transform.GetChild(1).gameObject.SetActive(true);
				persInv.GetComponent<PauseManager>().enabled = true;
				persInv.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
//				print ("creating new");
//				GameObject newPersInv = (GameObject)GameObject.Instantiate(persInv, transform.position, Quaternion.identity);
//				newPersInv.gameObject.transform.name = "persistentGM";
			}
			levelMan.ChangeLevel (savedLevel);
			//Application.LoadLevel (savedLevel);
		}
	}

	public void NewGame(){
		if (savedGame) {
			ShowScreen (2);
		} else {
			ShowScreen(5);
			Invoke ("StartTut", .1f);
			//LoadThisLevel(1);
		}
	}

	public void Launch (Rigidbody toLaunch){
		toLaunch.isKinematic = false;
		toLaunch.AddForce (forceToPush * forceMult);
		toLaunch.AddTorque (forceToPush * forceMult);
		toLaunch.gameObject.GetComponent<Animator> ().SetTrigger ("shrink");
	
	}
	
	public void ControlTutorial(){
		if (Input.GetButtonDown ("X")) {
			boolInteract = true;
			Launch(interactTut);
		}if (Input.GetButtonDown ("Y")) {
			boolSeed = true;
			Launch(seedTut);
		}if (Input.GetButtonDown ("B")) {
			boolCancel = true;
			Launch(cancelTut);
		}if (Input.GetButtonDown ("A")) {
			boolJump = true;
			Launch(jumpTut);
		}if (Input.GetButtonDown ("Pause")) {
			boolPause = true;
			Launch(pauseTut);
		}if (Input.GetButtonDown ("Inventory")) {
			boolInv = true;
			Launch(invTut);
		}if (Input.GetButtonDown ("CamIn") || Input.GetButtonDown("CamOut")) {
			boolCam = true;
			Launch(camTut);
		}if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0){
		    boolMove = true;
			Launch(moveTut);
		}





	}

	public void ReallyNewGame(){
		PlayerPrefs.DeleteAll ();
		ShowScreen(5);
		Invoke ("StartTut", .1f);
		//controlTut = true;
		//LoadThisLevel (1);
	}

	void StartTut(){
		controlTut = true;
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
		}if (screenToShow != 5) {
			eventSystem.SetSelectedGameObject (screenButtons[screenToShow] ,new BaseEventData(eventSystem));
		}

		if (screenToShow == 5) {
			title.SetActive(false);
		}
	}
}
