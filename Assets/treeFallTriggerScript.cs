using UnityEngine;
using System.Collections;

public class treeFallTriggerScript : MonoBehaviour {

	public Rigidbody[] rbsToActivate;
	public GameObject[] particles;

	public GameObject vineParticles, sub1, sub2;

	bool destroyedVines = false;

	float level;

	// Use this for initialization
	void Start () {
		level = Application.loadedLevel;
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider col) {
		if (level == 4) {
			if (!destroyedVines){
			DestroyVines ();
			}
		//	level = 3;
			if (col.transform.tag == "Player") {
			//	print ("activated");
				foreach (Rigidbody rb in rbsToActivate) {
					rb.isKinematic = false;
					rb.transform.GetComponent<MeshCollider> ().enabled = false;
					rb.gameObject.GetComponent<AudioSource> ().Play ();
					foreach (Transform child in rb.transform) {
						if (child.GetComponent<MeshCollider> () != null) {
							child.GetComponent<MeshCollider> ().enabled = false;
						}
					}
				}
			}

			foreach (GameObject part in particles) {
				part.SetActive (true);
			}
		}
	}

	void DestroyVines(){
		//print ("destroying vines");
		destroyedVines = true;
		GameObject[] vines = GameObject.FindGameObjectsWithTag ("Vine");

		foreach (GameObject vine in vines) {
			vine.GetComponent<vineGenerator>().sections = 0;
			vine.BroadcastMessage("Detach", SendMessageOptions.DontRequireReceiver);//gameObject.Find("AnchorFlower").GetComponent<AnchorFlowerScript>().Detach();
//			if (vine.tag != "UI"){
//				
//				//child.GetComponent<Collider>().isTrigger = true;
//				//Instantiate(vineParticles, vine.transform.position, Quaternion.identity);
//				vine.gameObject.SetActive(false);
//				if (Random.value < 0.5f){
//					//Instantiate(sub1, vine.transform.position, Quaternion.identity);
//				}if (Random.value < 0.5f){
//					//Instantiate(sub2, vine.transform.position, Quaternion.identity);
//				}
//			}
		}

	}
}
