using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
//[ExecuteInEditMode]
public class persistentInventory : MonoBehaviour {

	public Text[] seedInventory;

	public Text vineInventory, hudVineCount1, hudVineCount2;

	public GameObject inventoryCanvas;

	public int vineCount;
	public int 	roundTree, pointyTree, bush, 
				bareSapling, grass, flower,
				fence, sign, stump,
				rock, mushroom, barrel;

	public Transform currentVineUI;
	public Slider vineRemainingSlider;
	public Text vineUIQuant, vineUIShadow;

	void OnEnable(){
		LoadInventory ();
		FindInventoryAssociations ();
	}

	// Use this for initialization
	void Start () {
	
		if (Application.loadedLevel == 1) {
			FindInventoryAssociations();
		}
		if (Application.loadedLevel == 2) {
			FindInventoryAssociations();
		}if (Application.loadedLevel == 3) {
			FindInventoryAssociations();
		}if (Application.loadedLevel == 4) {
			FindInventoryAssociations();
		}if (Application.loadedLevel == 5) {
			FindInventoryAssociations();
		}
	}

	public void SaveInventory(){
		PlayerPrefs.SetInt ("roundTree", roundTree);
		PlayerPrefs.SetInt ("pointyTree", pointyTree);
		PlayerPrefs.SetInt ("bush", bush);
		PlayerPrefs.SetInt ("bareSapling", bareSapling);
		PlayerPrefs.SetInt ("grass", grass);
		PlayerPrefs.SetInt ("flower", flower);
		PlayerPrefs.SetInt ("fence", fence);
		PlayerPrefs.SetInt ("sign", sign);
		PlayerPrefs.SetInt ("stump", stump);
		PlayerPrefs.SetInt ("rock", rock);
		PlayerPrefs.SetInt ("mushroom", mushroom);
		PlayerPrefs.SetInt ("barrel", barrel);
	}

	public void LoadInventory(){
		roundTree = PlayerPrefs.GetInt ("roundTree");//, roundTree);
		pointyTree=PlayerPrefs.GetInt ("pointyTree");//, pointyTree);
		bush=PlayerPrefs.GetInt ("bush");//, bush);
		bareSapling=PlayerPrefs.GetInt ("bareSapling");//, bareSapling);
		grass=PlayerPrefs.GetInt ("grass");//, grass);
		flower=PlayerPrefs.GetInt ("flower");//, flower);
		fence=PlayerPrefs.GetInt ("fence");//, fence);
		sign=PlayerPrefs.GetInt("sign");//, sign);
		stump=PlayerPrefs.GetInt ("stump");//, stump);
        rock=PlayerPrefs.GetInt ("rock");//, rock);
        mushroom=PlayerPrefs.GetInt ("mushroom");//, mushroom);
		barrel=PlayerPrefs.GetInt("barrel");//, barrel);
	}

	void FindInventoryAssociations(){



		PlayerInventory playerInv = GameObject.Find ("Player").GetComponent<PlayerInventory> ();
		playerInv.currentVineUI = currentVineUI;
		playerInv.vineRemainingSlider = vineRemainingSlider;
		playerInv.vineUIQuant = vineUIQuant;
		playerInv.vineUIShadow = vineUIShadow;



		//PauseManager
//		print ("finding inventory associations");
//		inventoryCanvas = GameObject.Find ("InventoryCanvas");//.transform.root.GetChild (2).GetChild (4).gameObject;// GameObject.Find ("InventoryCanvas");
////		if (inventoryCanvas.activeSelf == false) {
////			inventoryCanvas.SetActive(true);
////		}
//		for (int i = 0; i<seedInventory.Length; i++) {
//			if (i==0){seedInventory[i] =  GameObject.Find ("colTex1").GetComponent<Text> ();}
//			if (i==1){seedInventory[i] =  GameObject.Find ("colTex2").GetComponent<Text> ();}
//			if (i==2){seedInventory[i] =  GameObject.Find ("colTex3").GetComponent<Text> ();}
//			if (i==3){seedInventory[i] =  GameObject.Find ("colTex4").GetComponent<Text> ();}
//			if (i==4){seedInventory[i] =  GameObject.Find ("colTex5").GetComponent<Text> ();}
//			if (i==5){seedInventory[i] =  GameObject.Find ("colTex6").GetComponent<Text> ();}
//			if (i==6){seedInventory[i] =  GameObject.Find ("colTex7").GetComponent<Text> ();}
//			if (i==7){seedInventory[i] =  GameObject.Find ("colTex8").GetComponent<Text> ();}
//			if (i==8){seedInventory[i] =  GameObject.Find ("colTex9").GetComponent<Text> ();}
//			if (i==9){seedInventory[i] =  GameObject.Find ("colTex10").GetComponent<Text> ();}
//			if (i==10){seedInventory[i] =  GameObject.Find("colTex11").GetComponent<Text> ();}
//			if (i==11){seedInventory[i] =  GameObject.Find("colTex12").GetComponent<Text> ();}
//		}
//		
//		vineInventory = GameObject.Find ("vineSeedCount").GetComponent<Text> ();
//		hudVineCount1 = GameObject.Find ("vineHud1").GetComponent<Text> ();
//		hudVineCount2 = GameObject.Find ("vineHud2").GetComponent<Text> ();
//		inventoryCanvas.SetActive (false);
	}
	
	void OnLevelWasLoaded(int level) {
		print ("level loaded " + Application.loadedLevel);

		FindInventoryAssociations ();

//		if (level == 2) {
//			foreach (GameObject prop in level1Objects){
//				if (prop != null){
//
//				}
//			}
//		}
	}

	public void AddSeed (int type){
		if (type == 0)
			roundTree ++;
		if (type == 1)
			pointyTree ++;
		if (type == 2)
			bush ++;
		if (type == 3)
			bareSapling ++;
		if (type == 4)
			grass ++;
		if (type == 5)
			flower ++;
		if (type == 6)
			fence ++;
		if (type == 7)
			sign ++;
		if (type == 8)
			stump ++;
		if (type == 9)
			rock ++;
		if (type == 10)
			mushroom ++;
		if (type == 11)
			barrel ++;
	}

	int QueryInventory(int seedToQuery){
		if (seedToQuery == 0) {
			return roundTree;
		}if (seedToQuery == 1) {
			return pointyTree;
		}if (seedToQuery == 2) {
			return bush;
		}if (seedToQuery == 3) {
			return bareSapling;
		}if (seedToQuery == 4) {
			return grass;
		}if (seedToQuery == 5) {
			return flower;
		}if (seedToQuery == 6) {
			return fence;
		}if (seedToQuery == 7) {
			return sign;
		}if (seedToQuery == 8) {
			return stump;
		}if (seedToQuery == 9) {
			return rock;
		}if (seedToQuery == 10) {
			return mushroom;
		}if (seedToQuery == 11) {
			return barrel;
		} else {
			print ("couldn't match query to an inventory item!");
			return 12;
		}
	}

	// Update is called once per frame
	void Update () {
		for (int i = 0; i<seedInventory.Length; i++) {
			seedInventory[i].text = "x " + QueryInventory(i);
		}
		vineInventory.text = "x "+vineCount;
		hudVineCount1.text = ""+vineCount;
		hudVineCount2.text = ""+vineCount;
	}
}
