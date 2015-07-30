using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInventory : MonoBehaviour {

	public bool haveBoatSeed, haveIslandSeed, haveHouseSeed;

	public Transform currentVineUI, currentVine;
	public vineGenerator vineGen;
	public Slider vineRemainingSlider;

	public int vineSeedQuant;
	public Text vineUIQuant, vineUIShadow;
	public GameObject vinePrefab;
	public bool canPlace, vineActive;
	public float vertOffset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		vineUIQuant.text = "" + vineSeedQuant;
		vineUIShadow.text = "" + vineSeedQuant;
		SeedInput ();
		if (vineGen != null) {
			if (vineGen.sections <= 0 && vineActive) {
				EndVine ();
			}
			if (vineGen.sections > 0) {
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
		currentVineUI.gameObject.SetActive(true);
		vineRemainingSlider.maxValue = vineGen.sections; 
		vineSeedQuant--;
		vineActive = true;
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
		vineSeedQuant ++;
	}

	public void GetSeedSmall(int type){
print ("figure out small seed inventory!");
	}

	public void GetBoatSeed(){
		print ("getting boat seed");
		haveBoatSeed = true;
		GameObject.Find ("mom").GetComponent<NPCDialogueScript> ().dialogueProgression = 2;
	}public void GetIslandSeed(){
		haveIslandSeed = true;
	}public void GetHouseSeed(){
		haveHouseSeed = true;
	}

}
