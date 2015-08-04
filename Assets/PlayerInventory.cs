using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInventory : MonoBehaviour {

	public bool haveMastSeed, haveBoatSeed, haveIslandSeed, haveHouseSeed;

//	public persistentInventory persInv;

	public Transform currentVineUI, currentVine;
	public vineGenerator vineGen;
	public Slider vineRemainingSlider;

	public int vineSeedQuant;
	public Text vineUIQuant, vineUIShadow;
	public GameObject vinePrefab;
	public bool canPlace, vineActive;
	public float vertOffset;

	public persistentInventory persInv;

	// Use this for initialization
	void Start () {
		persInv = GameObject.Find ("persistentGM").GetComponent<persistentInventory> ();
	}
	
	// Update is called once per frame
	void Update () {
		vineSeedQuant = persInv.vineCount;
		vineUIQuant.text = "" + vineSeedQuant;
		vineUIShadow.text = "" + vineSeedQuant;
		SeedInput ();
		if (vineGen != null) {
			if (vineGen.sections <= 0 && vineActive) {
				EndVine ();
			}
			else if (vineGen.sections > 0) {
				vineRemainingSlider.value = vineGen.sections;
			} 
		}
		if (vineActive) {
			bool below = vineGen.CheckIfBelow();
			if (below){
				EndVine();
			}
		}
		CheckForCancel ();
	}

	void CheckForCancel(){
		if (Input.GetButtonDown ("B") && vineActive) {
			EndVine();
		}
	}

	void EndVine(){
		vineGen.sections = 0;
		vineGen = null;
		currentVineUI.gameObject.SetActive(false);
		if (currentVineUI.gameObject.activeSelf) {
			currentVineUI.gameObject.SetActive(false);
		}
		currentVine = null;
		vineActive = false;
	}

	void SeedInput(){
		if (Input.GetButtonDown ("Y") && vineSeedQuant > 0 && canPlace && !vineActive) {
			PlaceVine();
		}
	}

	void PlaceVine(){
		GameObject newVine = (GameObject)Instantiate (vinePrefab, CalcPos (), Quaternion.identity);
		vineGen = newVine.GetComponent<vineGenerator> ();
		vineGen.player = transform;
		currentVine = newVine.transform;
		transform.position = new Vector3 (currentVine.position.x, transform.position.y, currentVine.position.z);
		currentVineUI.gameObject.SetActive(true);
		print ("placing");
		vineRemainingSlider.maxValue = vineGen.sections; 
		persInv.vineCount--;
		vineActive = true;
		if (Application.loadedLevel == 4) {

		}
	}

	Vector3 CalcPos(){
		Vector3 calculatedPos;
		calculatedPos.x = Mathf.RoundToInt (transform.position.x);
		calculatedPos.y = Mathf.RoundToInt (transform.position.y);
		calculatedPos.z = Mathf.RoundToInt (transform.position.z);
		calculatedPos.y -= vertOffset;
		if (calculatedPos.x % 2 == 0) {
			float test1 = calculatedPos.x + 1;
			float test2 = calculatedPos.x - 1;
			test1 = transform.position.x - test1;
			test2 = transform.position.x - test2;
			if (Mathf.Abs (test1) > Mathf.Abs (test2)){
				calculatedPos.x = calculatedPos.x - 1;
			}else{
				calculatedPos.x = calculatedPos.x + 1;
			}
		}if (calculatedPos.z % 2 == 0) {
			float test1 = calculatedPos.z + 1;
			float test2 = calculatedPos.z - 1;
			test1 = transform.position.z  - test1;
			test2 = transform.position.z  - test2;
			if (Mathf.Abs (test1) > Mathf.Abs (test2)){
				calculatedPos.z  = calculatedPos.z  - 1;
			}else{
				calculatedPos.z  = calculatedPos.z  + 1;
			}
		}
		return calculatedPos;
	}

	public void GetVineSeed(){
		persInv.vineCount++;
	}

	public void GetSeedSmall(int type){
		persInv.AddSeed (type);
	}
	public void GetMastSeed(){
		print ("getting mast seed");
		haveMastSeed = true;
		GameObject.Find ("mom").GetComponent<NPCDialogueScript> ().dialogueProgression = 1;
		GameObject.Find ("persistentGM").GetComponent<persistentInventory> ().QuestCollect (0);//mastImage.enabled = true;
	}
	public void GetBoatSeed(){
		print ("getting boat seed");
		haveBoatSeed = true;
		GameObject.Find ("mom").GetComponent<NPCDialogueScript> ().SetDialogue (3);// = 3;
		print ("found mom");
		GameObject.Find ("persistentGM").GetComponent<persistentInventory> ().QuestCollect (1);//.enabled = true;
	}public void GetIslandSeed(){
		haveIslandSeed = true;
		GameObject.Find ("persistentGM").GetComponent<persistentInventory> ().QuestCollect (2);//islandImage.enabled = true;
	}public void GetHouseSeed(){
		haveHouseSeed = true;
		GameObject.Find ("persistentGM").GetComponent<persistentInventory> ().QuestCollect (3);//houseImage.enabled = true;
	}

}
