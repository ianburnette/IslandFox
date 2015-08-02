using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour {

	public bool paused = false;
	public bool pauseScreen = false;
	public bool inventory = false;

	public GameObject pauseUI, inventoryUI;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Pause")){
			if (paused && !inventory){
				TogglePause();
				//PauseScreen(false);
			}else if (!paused && !inventory){
				TogglePause();
				//PauseScreen(true);
			}else if (paused && inventory){
				ToggleInventory(false);
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
		Time.timeScale = 1 - Time.timeScale;
		paused = !paused;
		PauseScreen(paused);
	}

	void PauseScreen(bool status){
		if (status) {
			pauseUI.SetActive (true);
			pauseScreen = true;
		} else {
			pauseUI.SetActive (false);
			pauseScreen = false;
		}

	}
}
