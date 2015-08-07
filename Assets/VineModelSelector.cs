using UnityEngine;
using System.Collections;

public class VineModelSelector : MonoBehaviour {

	public int before, after;
	public GameObject straight, end, corner;
	public GameObject normalAnchor, altAnchor;

	public AudioClip placeClip;
	public AudioSource source;

	public Sprite[] vineSprites;
	//public float vertOffset = .8f;
	
	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		source.PlayOneShot (placeClip, 1f);
		straight.SetActive (false);
		corner.SetActive (false);
		end.SetActive (true);
		if (before == 2) {
			end.transform.rotation = Quaternion.Euler(new Vector3(0,0,90));
		}
	}

	public void SetBefore(int lastBlockDir){
		before = lastBlockDir;
		if (before != 3) {
			if (before == 2)
				end.transform.rotation = Quaternion.Euler(new Vector3(0,0,90));
			else if (before == 4)
				end.transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
			else if (before == 5)
				end.transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
			else if (before == 6)
				end.transform.rotation = Quaternion.Euler(new Vector3(0,270,0));
		}
	//	print ("setbefore");
	}

	public void ChooseSprite (GameObject vineChunk){
		int rand = Random.Range (0, 3);
//		print ("vine rand is " + rand);
		vineChunk.transform.GetChild(rand).gameObject.SetActive(true);
		vineChunk.transform.GetChild (rand).GetComponent<SpriteRenderer> ().sprite = vineSprites [Random.Range (0, 3)];
	

	}

	public void SetAnchor(int dir){
		if (dir == 2) { //moved up
			//create sideways flower
			altAnchor = Resources.Load("AnchorChunk") as GameObject;
			GameObject anchor = (GameObject)GameObject.Instantiate(altAnchor, transform.position, Quaternion.identity);
			anchor.transform.parent = transform;
		} else {
			//create normal flower
			normalAnchor = Resources.Load("AnchorFlower") as GameObject;
			GameObject anchor = (GameObject)GameObject.Instantiate(normalAnchor, transform.position + (Vector3.up * 1.3f), Quaternion.identity);
			anchor.transform.parent = transform;
		}
	}

	public void SetAfter(int nextBlockDir){
		//print ("setafter");
		after = nextBlockDir;
		end.SetActive(false);
		if (before == 3 && after == 4) { //straight to right
			straight.SetActive (true);
			ChooseSprite(straight);
		} else if (before == 2 && after == 1) { //straight up
			straight.SetActive (true);
			straight.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));
			ChooseSprite(straight);
		}else if (before == 2 && after == 2) { //straight up
			straight.SetActive (true);
			straight.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));
			ChooseSprite(straight);
		} 

		else if (before == 4 && after == 3) { //straight to left
			straight.SetActive (true);
			straight.transform.rotation = Quaternion.Euler (new Vector3 (0, 180, 0));
			ChooseSprite(straight);
		}
		else if (before == 4 && after == 4) { //back to right
			end.SetActive (true);
			end.transform.rotation = Quaternion.Euler (new Vector3 (0, 180, 0));
		}
		else if (before == 3 && after == 3) { //back to left
			end.SetActive (true);
			end.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
		}else if (before == 5 && after == 5) { //back to forward
			end.SetActive (true);
			end.transform.rotation = Quaternion.Euler (new Vector3 (0, 90, 0));
		}else if (before == 6 && after == 6) { //back to back
			end.SetActive (true);
			end.transform.rotation = Quaternion.Euler (new Vector3 (0, 270, 0));
		}
		else if (before == 5 && after == 6) { //moving back
			straight.SetActive (true);
			straight.transform.rotation = Quaternion.Euler (new Vector3 (0, 90, 0));
			ChooseSprite(straight);
		} else if (before == 6 && after == 5) { //moving forward
			straight.SetActive (true);
			straight.transform.rotation = Quaternion.Euler (new Vector3 (0, 270, 0));
			ChooseSprite(straight);
		} else if (before == 2 && after == 3) { //moving up and turning right
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (0, 90, 270));
			ChooseSprite(corner);
		} else if (before == 2 && after == 4) { //moving up and turning left
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (270, 0, 0));
			ChooseSprite(corner);
		} else if (before == 2 && after == 5) { //moving up and turning forward
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (270, 270, 0));
			ChooseSprite(corner);
		} else if (before == 2 && after == 6) { //moving up and turning forward
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (270, 90, 0));
			ChooseSprite(corner);
		}

			else if (before == 3 && after == 6) { //turning from moving right to moving back
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (0, 90, 0));
			ChooseSprite(corner);
		} else if (before == 3 && after == 5) { //turning from moving right to moving forward
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (0, 180, 0));
			ChooseSprite(corner);
		} else if (before == 3 && after == 2) { //turning from moving right to moving up
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (90, 180, 0));
			ChooseSprite(corner);
		} 
		else if (before == 4 && after == 6) { //turning from moving left to moving back
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
			ChooseSprite(corner);
		} else if (before == 4 && after == 5) { //turning from moving left to moving forward
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (0, 270, 0));
			ChooseSprite(corner);
		} else if (before == 4 && after == 2) { //turning from moving left to moving up
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (90, 0, 0));
			ChooseSprite(corner);
		} 
		else if (before == 5 && after == 4) { //turning from moving back to moving right
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (0, 270, 0));
			ChooseSprite(corner);
		} else if (before == 5 && after == 3) { //turning from moving back to moving left
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (0, 180, 0));
			ChooseSprite(corner);
		} else if (before == 5 && after == 2) { //turning from moving back to moving up
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (0, 180, 90));
			ChooseSprite(corner);
		} 
		else if (before == 6 && after == 4) { //turning from moving forward to moving right
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
			ChooseSprite(corner);
		} else if (before == 6 && after == 3) { //turning from moving forward to moving left
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (0, 90, 0));
			ChooseSprite(corner);
		} else if (before == 6 && after == 2) { //turning from moving forward to moving up
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (90, 90, 0));
			ChooseSprite(corner);
		}
	}
}
