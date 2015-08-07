using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//[ExecuteInEditMode]
public class NPCDialogueScript : MonoBehaviour {

	public int dialogueProgression = 0;
	public int[] dialogueIndex;

	bool hardSet;

	public bool mom, end;

	public GameObject gm;
	public Transform player;

	public GameObject dialoguePanels;

	public GameObject boat, endBoat;


	public Text dialoguePanelContents, dialogueSpeakerName;



	public string[] dialogue1;
	public string[] speaker1;

	public string[] dialogue2;
	public string[] speaker2;

	public string[] dialogue3;
	public string[] speaker3;

	public string[] dialogue4;
	public string[] speaker4;

	public bool readyToStart;
	public GameObject buttonPrompt;	

	private bool _showing;
	private string _text;

	public AudioClip nextClip;
	public AudioSource source;

	public int skipBackIndex;

	void OnEnable(){
		nextClip = Resources.Load ("click3") as AudioClip;

		gm = GameObject.Find ("persistentGM");
		player = GameObject.Find ("Player").transform;
		source = player.GetComponent<AudioSource> ();
		Setup ();
//		Dialoguer.events.onStarted += onStarted;
//		Dialoguer.events.onEnded += onEnded;
//		Dialoguer.events.onTextPhase += onTextPhase;
	}
	
	void Setup(){
		dialoguePanels = gm.transform.GetChild (0).GetChild (0).gameObject;
		dialoguePanels.SetActive (false);
		dialoguePanelContents = dialoguePanels.transform.GetChild (1).GetChild (0).GetComponent<Text> ();
		dialogueSpeakerName = dialoguePanels.transform.GetChild (0).GetChild (0).GetComponent<Text> ();
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
		if ((dialogueProgression == 0 || dialogueProgression == 1 || dialogueProgression == 2) && !mom && dialogueIndex.Length>dialogueProgression+1) {
//			print ("dialogue length is " + dialogueIndex.Length + " and dialogue progression is " + dialogueProgression);
			dialogueProgression ++;
		} else if (dialogueProgression == 1 && mom) {
			dialogueProgression++;
		}
		else if (dialogueProgression == 3 && mom) {
			SpawnBoat();
		} if (end) {
			SwitchBoat();
		}
	}

	void SwitchBoat(){
		boat.SetActive (false);
		endBoat.transform.parent = null;
		endBoat.SetActive (true);
	}

	void SpawnBoat(){
		boat.SetActive (true);
	}
	
	void onTextPhase(DialoguerTextData data){
		dialoguePanelContents.text = data.text;
		dialogueSpeakerName.text = data.name;
	}

	void ShowPanels (bool state){
		//GameObject.Find ("UI").transform.GetChild (0).gameObject.SetActive (state);
		dialoguePanels.SetActive (state);
	}

	void StartDialogue (int progression){
		onStarted ();
		ShowDialogue ();
	}

	void ShowDialogue(){
		source.PlayOneShot(nextClip, 1f);
		if (dialogueProgression == 0) {
			dialoguePanelContents.text = dialogue1[dialogueIndex[0]];
			dialogueSpeakerName.text = speaker1[dialogueIndex[0]];
		}if (dialogueProgression == 1) {
			//print ("trying to show");
			///print (dialogue2[dialogueIndex[1]]);
			dialoguePanelContents.text = dialogue2[dialogueIndex[1]];
			dialogueSpeakerName.text = speaker2[dialogueIndex[1]];
		}if (dialogueProgression == 2) {
			dialoguePanelContents.text = dialogue3[dialogueIndex[2]];
			dialogueSpeakerName.text = speaker3[dialogueIndex[2]];
		}if (dialogueProgression == 3) {
			dialoguePanelContents.text = dialogue4[dialogueIndex[3]];
			dialogueSpeakerName.text = speaker4[dialogueIndex[3]];
		}
	}

	void ContinueDialogue(){
		if (!Ended ()) {
			dialogueIndex[dialogueProgression]++;
			source.PlayOneShot(nextClip, 1f);
			ShowDialogue ();
		} else {
			EndDialogue();
		}
	}

	void EndDialogue(){
		if (dialogueProgression == dialogueIndex.Length - 1) {//at end of possible dialogues
			dialogueIndex[dialogueProgression] = skipBackIndex;
		}
		source.PlayOneShot(nextClip, 1f);
		onEnded ();
	}

	bool Ended(){
		if (dialogueProgression == 0) {
			if (dialogueIndex[dialogueProgression] < dialogue1.Length-1)
				return false;
			else
				return true;
		}else if (dialogueProgression == 1) {
			if (dialogueIndex[dialogueProgression] < dialogue2.Length-1)
				return false;
			else
				return true;
		}else if (dialogueProgression == 2) {
			if (dialogueIndex[dialogueProgression] < dialogue3.Length-1)
				return false;
			else
				return true;
		}else if (dialogueProgression == 3) {
			if (dialogueIndex[dialogueProgression] < dialogue4.Length-1)
				return false;
			else
				return true;
		}
		print ("dialogue logic error 1b");
		return false;
	}

	public void SetDialogue(int setTo){
		print ("Setting to " + setTo);
		dialogueProgression = setTo;
		hardSet = true;
	}

	// Update is called once per frame
	void Update () {
		if (hardSet) {
			dialogueProgression = 3;
		}
		if (readyToStart && buttonPrompt.activeSelf == false && !_showing) {
			buttonPrompt.SetActive(true);// = true;
		}if (!readyToStart && buttonPrompt.activeSelf) {
			buttonPrompt.SetActive(false);// = false;
		}if (_showing && buttonPrompt.activeSelf == true) {
			buttonPrompt.SetActive(false);// = false;
		}

		if (readyToStart && Input.GetButtonDown ("X")) {
			buttonPrompt.SetActive(false);
			StartDialogue(dialogueProgression);
			//print ("starting " + (dialogueIndex[dialogueProgression]));
			//Dialoguer.StartDialogue(dialogueIndex[dialogueProgression], DialoguerCallback);
		}

		if (_showing && Input.GetButtonDown ("A")) {
			ContinueDialogue();
		}
	}

	void DialoguerCallback(){
		buttonPrompt.SetActive (true);
	}

	void OnTriggerStay(Collider col){
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
				player.GetComponent<PlayerInventory>().enabled=false;
				player.GetComponent<cameraChanger>().enabled=false;
				player.GetComponent<Rigidbody>().isKinematic = true;
				player.GetComponent<Animator>().SetBool("moving", false);
			}else{
				player.GetComponent<Health>().enabled=true;
				player.GetComponent<PlayerMove>().enabled=true;
				player.GetComponent<PlayerInventory>().enabled=true;
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
