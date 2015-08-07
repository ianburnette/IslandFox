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

	public bool reachedTree;

	public GameObject eventSystem;

	void Awake(){
		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);
		}
	}

	void OnLevelWasLoaded(int level){
		loadingText.SetActive (false);
		if (level != 0) {
			eventSystem.SetActive(true);
			//Destroy (gameObject);
		}
		if (level != 0 && level != 1) {
			PlayerPrefs.SetInt("SavedLevel", level);
		}
		if (level == 4 && reachedTree) {
			Transform player = GameObject.Find ("Player").transform;
			player.position = GameObject.Find ("spawnLocation1").transform.position;
			Transform boat = GameObject.Find ("boat").transform;
			boat.position = GameObject.Find ("spawnLocation0").transform.position;
		}
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
			GameObject.Find ("EventSystem").SetActive (false);
			//print ("deactivating events");
			GameObject.Find ("HUDInventoryCanvas").SetActive (false);
			GetComponent<PauseManager> ().enabled = false;
		} else {
			GetComponent<PauseManager>().enabled=true;
		}

	}


	void FadeOut(){
		targetAlpha = 1f;
	//	loadingText.SetActive (true);
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
			if (loadingText.activeSelf == true) {
				loadingText.SetActive (false);
			}
		}else if (fadeImage.color.a < targetAlpha && changeLevel) {
			if (loadingText.activeSelf == false){
				loadingText.SetActive(true);
			}

			fadeImage.color = new Color (fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a + (fadeIncrement * fadeTime));
		}
		if (changeLevel && fadeImage.color.a >= targetAlpha ) {
//		print ("changing?");
			//loadingText.SetActive(true);
			changeLevel = false;
			GameObject.Find ("persistentGM").GetComponent<persistentGMScript>().comingFrom = Application.loadedLevel;
			Application.LoadLevel(levelToLoad);
			targetAlpha = 0f;
		
		}
		if (loadingText.activeSelf == true && fadeImage.color.a < targetAlpha) {
			loadingText.SetActive (false);
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
