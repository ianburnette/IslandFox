using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class endGame : MonoBehaviour {

	public bool showingUI;
	public GameObject endUI;
	public GameObject player;
	public Image fadeImage;
	public float targetAlpha;
	public float fadeIncrement;// = 0.01f;
	float fadeTime = 1f;
	public bool continueEnd = false;
	public bool endedOnce;

	public Material newSkybox, blueGrass;
	public GameObject oldLights, newLights;
	public Renderer grassRenderer;
	public GameObject lowerClouds, rain;
	public Color nightSpriteColor;
	public GameObject vineUI;

	public persistentAudio persAud;

	public GameObject homeIsland;

	public GameObject[] bonusFlowers;

	public levelManager levelMan;

	// Use this for initialization
	void Start () {
		levelMan = GameObject.Find ("persistentGM").GetComponent<levelManager> ();
		vineUI = levelMan.gameObject.transform.GetChild (0).GetChild (1).gameObject;
		fadeImage = levelMan.gameObject.transform.GetChild (0).GetChild (2).GetChild (0).gameObject.GetComponent<Image> ();
		persAud = GameObject.Find ("persistentAudioGM").GetComponent<persistentAudio> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("X") && showingUI && !endedOnce) {
			SetupEnd();
		}if (Input.GetButtonDown ("X") && showingUI && endedOnce) {
			print ("ending?");
			fadeIncrement = 0f;
			levelMan.fadeIncrement = .01f;
			levelMan.ChangeLevel(0);
		}
		Fade ();

	}

	void Fade(){

		if (fadeImage.color.a > targetAlpha) {
			fadeImage.color = new Color (fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a - (fadeIncrement * fadeTime));
		} else if (fadeImage.color.a < targetAlpha+0.1f) {
		
			fadeImage.color = new Color (fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a + (fadeIncrement  * fadeTime));
		} 

		if (fadeImage.color.a > 0.99f && !continueEnd) {
			ContinueEnd();
			persAud.targetClip = persAud.level5C;
			continueEnd = true;
		}
		print (fadeImage.color.a);
	}

	void SetupEnd(){
		targetAlpha = 1f;
		levelMan.fadeIncrement = 0f;
	}

	void ContinueEnd(){
		print ("continueing");
		RenderSettings.skybox = newSkybox;
		RenderSettings.fog = false;
		grassRenderer.material = blueGrass;
		oldLights.SetActive (false);
		newLights.SetActive (true);
		Invoke ("Unfade", 2f);
		rain.SetActive (false);
		lowerClouds.SetActive (false);
		foreach (GameObject flower in bonusFlowers) {
			flower.SetActive(true);
		}
		SpriteRenderer[] sprites = FindObjectsOfType(typeof(SpriteRenderer)) as SpriteRenderer[];
		foreach (SpriteRenderer sprt in sprites) {
			sprt.color = nightSpriteColor;
		}
		homeIsland.SetActive (false);
	}

	void Unfade(){
		player.GetComponent<PlayerInventory> ().enabled = true;
		endedOnce = true;
		vineUI.SetActive (true);

		targetAlpha = 0f;
		player.transform.position += Vector3.back * 3f;
		player.GetComponent<playerIslandGrow> ().OutsideAtNight ();
		transform.GetChild (0).GetChild (0).GetChild (0).GetChild (1).GetComponent<Text> ().text = "End Game";
		transform.GetChild (0).GetChild (0).GetChild (0).GetChild (2).GetComponent<Text> ().text = "End Game";
	}

	void OnTriggerEnter(Collider col){
		if (col.transform.tag == "Player") {
			if (!showingUI){
				endUI.SetActive(true);
				showingUI = true;
			}
		}
	}

	
	void OnTriggerExit(Collider col){
		if (col.transform.tag == "Player") {
			if (showingUI){
				endUI.SetActive(false);
				showingUI = false;
			}
		}
	}
}
