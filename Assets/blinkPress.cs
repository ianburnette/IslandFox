using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class blinkPress : MonoBehaviour {

	public float blinkTime;
	public Image buttonImage;
	public Sprite unpressed, pressed;

	void Start(){
		buttonImage = GetComponent<Image> ();
	}

	// Use this for initialization
	void OnEnable () {
		InvokeRepeating ("Press", 0, blinkTime);
		InvokeRepeating ("UnPress", blinkTime+(blinkTime/2), blinkTime);
	}

	void OnDisable () {
		CancelInvoke ("Press");//, 0, blinkTime);
		CancelInvoke ("UnPress");//, blinkTime+(blinkTime/2), blinkTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Press(){
		buttonImage.sprite = pressed;
//		print ("press");
	}

	void UnPress(){
		buttonImage.sprite = unpressed;
//		print ("unpress");
	}

}
