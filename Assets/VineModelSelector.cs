using UnityEngine;
using System.Collections;

public class VineModelSelector : MonoBehaviour {

	public int before, after;
	public GameObject straight, end, corner;

	void OnLevelWasLoaded(){

	}

	// Use this for initialization
	void Start () {
		straight.SetActive (false);
		corner.SetActive (false);
		end.SetActive (true);
		if (before == 2) {
			end.transform.rotation = Quaternion.Euler(new Vector3(0,0,90));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
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
	}

	public void SetAfter(int nextBlockDir){
		after = nextBlockDir;
		end.SetActive(false);
		if (before == 3 && after == 4) { //straight to right
			straight.SetActive (true);
		} else if (before == 2 && after == 1) { //straight up
			straight.SetActive (true);
			straight.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));
		}else if (before == 2 && after == 2) { //straight up
			straight.SetActive (true);
			straight.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));
		} 

		else if (before == 4 && after == 3) { //straight to left
			straight.SetActive (true);
			straight.transform.rotation = Quaternion.Euler (new Vector3 (0, 180, 0));
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
		} else if (before == 6 && after == 5) { //moving forward
			straight.SetActive (true);
			straight.transform.rotation = Quaternion.Euler (new Vector3 (0, 270, 0));
		} else if (before == 2 && after == 3) { //moving up and turning right
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (0, 90, 270));
		} else if (before == 2 && after == 4) { //moving up and turning left
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (270, 0, 0));
		} else if (before == 2 && after == 5) { //moving up and turning forward
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (270, 270, 0));
		} else if (before == 2 && after == 6) { //moving up and turning forward
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (270, 90, 0));
		}

			else if (before == 3 && after == 6) { //turning from moving right to moving back
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (0, 90, 0));
		} else if (before == 3 && after == 5) { //turning from moving right to moving forward
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (0, 180, 0));
		} else if (before == 3 && after == 2) { //turning from moving right to moving up
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (90, 180, 0));
		} 
		else if (before == 4 && after == 6) { //turning from moving left to moving back
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
		} else if (before == 4 && after == 5) { //turning from moving left to moving forward
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (0, 270, 0));
		} else if (before == 4 && after == 2) { //turning from moving left to moving up
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (90, 0, 0));
		} 
		else if (before == 5 && after == 4) { //turning from moving back to moving right
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (0, 270, 0));
		} else if (before == 5 && after == 3) { //turning from moving back to moving left
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (0, 180, 0));
		} else if (before == 5 && after == 2) { //turning from moving back to moving up
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (0, 180, 90));
		} 
		else if (before == 6 && after == 4) { //turning from moving forward to moving right
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
		} else if (before == 6 && after == 3) { //turning from moving forward to moving left
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (0, 90, 0));
		} else if (before == 6 && after == 2) { //turning from moving forward to moving up
			corner.SetActive(true);
			corner.transform.rotation = Quaternion.Euler (new Vector3 (90, 90, 0));
		}
	}
}
