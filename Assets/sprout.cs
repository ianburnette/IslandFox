using UnityEngine;
using System.Collections;

public class sprout : MonoBehaviour {

	public float waitTime;
	public persistentInventory inventory;
	public GameObject flower;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().enabled = false;
	//	Invoke ("ShowSprout", waitTime);
		ShowSprout ();
		inventory = GameObject.Find ("persistentGM").GetComponent<persistentInventory> ();
	}
	
	// Update is called once per frame
	void ShowSprout () {
		GetComponent<SpriteRenderer> ().enabled = true;
		InvokeRepeating ("CheckToCreate", waitTime * 2, waitTime);
	}

	void CheckToCreate(){
		if (inventory.vineCount < 5) {
			CreateFlower();
		}
	}

	void CreateFlower(){
		Instantiate (flower, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}
