using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerIslandGrow : MonoBehaviour {

	public int currentItemToPlace = 0;

	public bool haveBoatSeed, haveIslandSeed, haveHouseSeed;

	public GameObject seedPlantingHUD, vineHUD;
	public Image currentSeedImage;
	public Text currentCount1, currentCount2;

	public GameObject[] roundTrees, pointyTrees, bushes, bareSaplings,
		grasses, flowers, fences, signs, stumps,
		rocks, mushrooms, barrels;

	public persistentInventory persInv;
	
	//public Transform currentVineUI, currentVine;
	//public vineGenerator vineGen;
	//public Slider vineRemainingSlider;
	
	//public int vineSeedQuant;
	//public Text vineUIQuant, vineUIShadow;
	//public GameObject vinePrefab;
	public bool canPlace;//, vineActive;
	public float vertOffset;

	public float currentCount;

	// Use this for initialization
	void Start () {
		persInv = GameObject.Find ("persistentGM").GetComponent<persistentInventory> ();
	}

	void OnEnable(){
		vineHUD.SetActive (false);
	}

	void SeedInput(){
		if (Input.GetButtonDown ("Y") && canPlace) {
			PlaceVine();
		}
	}

	void UpdateCurrentCount(){
		if (currentItemToPlace == 0) {
			currentCount = persInv.roundTree;
		}if (currentItemToPlace == 1) {
			currentCount = persInv.pointyTree;
		}if (currentItemToPlace == 2) {
			currentCount = persInv.bush;
		}if (currentItemToPlace == 3) {
			currentCount = persInv.bareSapling;
		}if (currentItemToPlace == 4) {
			currentCount = persInv.grass;
		}if (currentItemToPlace == 5) {
			currentCount = persInv.flower;
		}if (currentItemToPlace == 6) {
			currentCount = persInv.fence;
		}if (currentItemToPlace == 7) {
			currentCount = persInv.sign;
		}if (currentItemToPlace == 8) {
			currentCount = persInv.stump;
		}if (currentItemToPlace == 9) {
			currentCount = persInv.rock;
		}if (currentItemToPlace == 10) {
			currentCount = persInv.mushroom;
		}if (currentItemToPlace == 11) {
			currentCount = persInv.barrel;
		}

	}

	void DecrementCurrentCount(){
		if (currentItemToPlace == 0) {
			persInv.roundTree--;
		}if (currentItemToPlace == 1) {
			persInv.pointyTree--;
		}if (currentItemToPlace == 2) {
			persInv.bush--;
		}if (currentItemToPlace == 3) {
			persInv.bareSapling--;
		}if (currentItemToPlace == 4) {
			persInv.grass--;
		}if (currentItemToPlace == 5) {
			persInv.flower--;
		}if (currentItemToPlace == 6) {
			persInv.fence--;
		}if (currentItemToPlace == 7) {
			persInv.sign--;
		}if (currentItemToPlace == 8) {
			persInv.stump--;
		}if (currentItemToPlace == 9) {
			persInv.rock--;
		}if (currentItemToPlace == 10) {
			persInv.mushroom--;
		}if (currentItemToPlace == 11) {
			persInv.barrel--;
		}
		
	}

	// Update is called once per frame
	void Update () {

		UpdateCurrentCount ();

		currentCount1.text = "" + currentCount;
		currentCount2.text = "" + currentCount;

		currentSeedImage.sprite = persInv.seedInventory [currentItemToPlace].transform.parent.GetChild (2).GetComponent<Image> ().sprite;
		if (currentCount <= 0) {
			currentItemToPlace++;
		}

//		vineSeedQuant = persInv.vineCount;
//		vineUIQuant.text = "" + vineSeedQuant;
//		vineUIShadow.text = "" + vineSeedQuant;
		SeedInput ();
//		if (vineGen != null) {
//			if (vineGen.sections <= 0 && vineActive) {
//				EndVine ();
//			}
//			else if (vineGen.sections > 0) {
//				vineRemainingSlider.value = vineGen.sections;
//			} 
//		}
//		if (vineActive) {
//			bool below = vineGen.CheckIfBelow();
//			if (below){
//				EndVine();
//			}
//		}
//		CheckForCancel ();
	}
	
//	void CheckForCancel(){
//		if (Input.GetButtonDown ("B") && vineActive) {
//			EndVine();
//		}
//	}
	
//	void EndVine(){
//		vineGen.sections = 0;
//		vineGen = null;
//		currentVineUI.gameObject.SetActive(false);
//		if (currentVineUI.gameObject.activeSelf) {
//			currentVineUI.gameObject.SetActive(false);
//		}
//		currentVine = null;
//		vineActive = false;
//	}
	

	
	void PlaceVine(){

		GameObject currentPrefab = gameObject;
		if (currentItemToPlace == 0) {
			currentPrefab = roundTrees [Random.Range (0, roundTrees.Length)];
		} else if (currentItemToPlace == 1) {
			currentPrefab = pointyTrees [Random.Range (0, pointyTrees.Length)];
		} else if (currentItemToPlace == 2) {
			currentPrefab = bushes [Random.Range (0, bushes.Length)];
		} else if (currentItemToPlace == 3) {
			currentPrefab = bareSaplings [Random.Range (0, bareSaplings.Length)];
		} else if (currentItemToPlace == 4) {
			currentPrefab = grasses [Random.Range (0, grasses.Length)];
		} else if (currentItemToPlace == 5) {
			currentPrefab = flowers [Random.Range (0, flowers.Length)];
		} else if (currentItemToPlace == 6) {
			currentPrefab = fences [Random.Range (0, fences.Length)];
		} else if (currentItemToPlace == 7) {
			currentPrefab = signs [Random.Range (0, signs.Length)];
		} else if (currentItemToPlace == 8) {
			currentPrefab = stumps [Random.Range (0, stumps.Length)];
		} else if (currentItemToPlace == 9) {
			currentPrefab = rocks [Random.Range (0, rocks.Length)];
		} else if (currentItemToPlace == 10) {
			currentPrefab = mushrooms [Random.Range (0, mushrooms.Length)];
		} else if (currentItemToPlace == 11) {
			currentPrefab = barrels [Random.Range (0, barrels.Length)];
		}
	


		GameObject newPlant = (GameObject)Instantiate (currentPrefab, CalcPos (), Quaternion.identity);
		newPlant.GetComponent<pickupProp> ().containsSeed = false;

		if (currentItemToPlace == 6) {
			newPlant.transform.Rotate(0,90,0);
		}
	
//		vineGen = newVine.GetComponent<vineGenerator> ();
//		vineGen.player = transform;
//		currentVine = newVine.transform;
//		currentVineUI.gameObject.SetActive(true);
//		vineRemainingSlider.maxValue = vineGen.sections; 
		DecrementCurrentCount ();
		//vineActive = true;
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
