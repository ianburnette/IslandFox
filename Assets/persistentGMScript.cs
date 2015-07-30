using UnityEngine;
using System.Collections;

public class persistentGMScript : MonoBehaviour {

	public int comingFrom;
	public Transform player;

	// Use this for initialization
	void Start () {
		GameObject.DontDestroyOnLoad (gameObject);
	}

	void OnLevelWasLoaded(int level){

		print ("loading level number " + level  + ", coming from " + comingFrom);
		SetPlayerLocation(level);
	}

	void SetPlayerLocation(int levelToSet){
		if (levelToSet == 0) {
			if (comingFrom == 1) {
				FindPlayer ();
				player.transform.position = GameObject.Find ("spawnLocation0").transform.position;
			} else if (comingFrom == 2) {
				FindBoat ();
				player.transform.position = GameObject.Find ("spawnLocation1").transform.position;
			}
		} else if (levelToSet == 1) {
			FindPlayer ();
			player.transform.position = GameObject.Find ("spawnLocation0").transform.position;
		} else if (levelToSet == 2) {
			if (comingFrom == 2) {
				FindBoat ();
				player.transform.position = GameObject.Find ("spawnLocation0").transform.position;
			} else if (comingFrom == 3) {
				FindBoat ();
				player.transform.position = GameObject.Find ("spawnLocation1").transform.position;
			}
		}else if (levelToSet == 3) {
			FindBoat ();
			player.transform.position = GameObject.Find ("spawnLocation0").transform.position;
		}

	}

	void FindBoat(){
		player = GameObject.Find ("boat").transform;
	}

	void FindPlayer(){
		
		player = GameObject.Find ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
