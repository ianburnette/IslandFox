﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPCDialogueScript : MonoBehaviour {

	public GameObject dialoguePanels;

	public Transform player;

	public Text dialoguePanelContents, dialogueSpeakerName;

	public string dialogueName;

	public bool readyToStart;
	public GameObject buttonPrompt;	

	private bool _showing;
	private string _text;
	
	void Start(){
		Dialoguer.events.onStarted += onStarted;
		Dialoguer.events.onEnded += onEnded;
		Dialoguer.events.onTextPhase += onTextPhase;
	}

	void onStarted () {
		_showing = true;
		ShowPanels (true);
		FreezePlayer (true);
		Camera.main.GetComponent<customCameraControls> ().Dialogue (true);
	}
	
	void onEnded () {
		_showing = false;	
		ShowPanels (false);
		FreezePlayer (false);
		Camera.main.GetComponent<customCameraControls> ().Dialogue (false);
	}
	
	void onTextPhase(DialoguerTextData data){
		dialoguePanelContents.text = data.text;
		dialogueSpeakerName.text = data.name;
	}

	void ShowPanels (bool state){
		dialoguePanels.SetActive (state);
	}

	// Update is called once per frame
	void Update () {
		if (readyToStart && buttonPrompt.activeSelf == false && !_showing) {
			buttonPrompt.SetActive(true);// = true;
		}if (!readyToStart && buttonPrompt.activeSelf) {
			buttonPrompt.SetActive(false);// = false;
		}if (_showing && buttonPrompt.activeSelf == true) {
			buttonPrompt.SetActive(false);// = false;
		}

		if (readyToStart && Input.GetButtonDown ("Fire1")) {
			buttonPrompt.SetActive(false);
			Dialoguer.StartDialogue(0, DialoguerCallback);
		}

		if (_showing && Input.GetButtonDown ("Fire2")) {
			Dialoguer.ContinueDialogue();
		}
	}

	void DialoguerCallback(){
		buttonPrompt.SetActive (true);
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player") {
			player = col.transform;
			readyToStart = true;
		}
	}

	void FreezePlayer(bool state){
		if (player != null) {
			if (state == true){
				player.GetComponent<Health>().enabled=false;
				player.GetComponent<PlayerMove>().enabled=false;
				player.GetComponent<cameraChanger>().enabled=false;
				player.GetComponent<Rigidbody>().isKinematic = true;
				player.GetComponent<Animator>().SetBool("moving", false);
			}else{
				player.GetComponent<Health>().enabled=true;
				player.GetComponent<PlayerMove>().enabled=true;
				player.GetComponent<cameraChanger>().enabled=true;
				player.GetComponent<Rigidbody>().isKinematic = false;
			}
		}
	}

	void OnTriggerExit(Collider col){
		if (col.tag == "Player") {
			readyToStart = false;
		}
	}
}
