﻿using UnityEngine;
using System.Collections;

public class vineGenerator : MonoBehaviour {

	public Transform vineSection;
	public int sections;
	public Transform player;
	public bool generating;
	public float differenceMargin, anchorOffset;
	public Vector3 mostRecent, lastDiff, currentDiff;
	public Transform straight, straightEnd, corner;
	public Transform latestChunk, UIprompt;
	public float vertMargin = 2f;
	public float vertGrowDist = 3f;
	bool spawnedAnchor;

	bool placedAnchor= false;

	public AudioClip destroyClip, crumbleClip;
	public AudioSource source;

	public void DestroyAudio(){
		source.PlayOneShot (destroyClip);
		source.PlayOneShot (crumbleClip, .5f);
	}

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		mostRecent = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (generating && sections > 0) {
			Generate();
		}
		if (sections <= 0 && !spawnedAnchor) {
			//SpawnAnchor();
			spawnedAnchor = true;
		}
		//CheckIfBelow ();
	}

	void SpawnAnchor(){
//		Transform anchorFlowerInstance = (Transform)Instantiate (anchorFlower, transform.position + (Vector3.up * anchorOffset), Quaternion.identity);
//		anchorFlowerInstance.parent = transform;
	}

	public bool CheckIfBelow(){
		if (player.position.y < mostRecent.y - vertMargin) {
			return true;
		} else {
			return false;
		}
	}

	void Generate(){
		if (player.position.x > mostRecent.x + differenceMargin) {
			CreateNew(Vector3.right * 2f);
		}else if (player.position.x < mostRecent.x - differenceMargin) {
			CreateNew(Vector3.left * 2f);
		}else if (player.position.z > mostRecent.z + differenceMargin) {
			CreateNew(Vector3.forward * 2f);
		}else if (player.position.z < mostRecent.z - differenceMargin) {
			CreateNew(Vector3.back * 2f);
		}else if (player.position.y > mostRecent.y + differenceMargin * vertGrowDist) {
			CreateNew(Vector3.up * 2f);
		}
	}

	void CreateNew (Vector3 newPosition){
		lastDiff = currentDiff;
		currentDiff = newPosition;
		if (latestChunk.transform != null) {
			SetLastModel (currentDiff);
		}
		latestChunk = (Transform)Instantiate (vineSection, mostRecent + newPosition, Quaternion.identity);
		latestChunk.parent = transform;
		SetThisModel (newPosition);
		mostRecent = latestChunk.position;
		sections--;
	}

	void SetThisModel(Vector3 prevPos){
		if (prevPos == Vector3.up * 2f) 
			latestChunk.GetComponent<VineModelSelector>().SetBefore(2);
		if (prevPos == Vector3.left * 2f) 
			latestChunk.GetComponent<VineModelSelector>().SetBefore(4);
		if (prevPos == Vector3.right * 2f) 
			latestChunk.GetComponent<VineModelSelector>().SetBefore(3);
		if (prevPos == Vector3.forward * 2f) 
			latestChunk.GetComponent<VineModelSelector>().SetBefore(6);
		if (prevPos == Vector3.back * 2f) 
			latestChunk.GetComponent<VineModelSelector>().SetBefore(5);
//		print (prevPos);
	}

	void SetLastModel(Vector3 newPos){
		if (newPos == Vector3.up * 2f) {
			latestChunk.GetComponent<VineModelSelector> ().SetAfter (2);
			if (!placedAnchor) {
				latestChunk.GetComponent<VineModelSelector>().SetAnchor(2);
				placedAnchor = true;
			}
		}
		if (newPos == Vector3.left * 2f) {
			latestChunk.GetComponent<VineModelSelector> ().SetAfter (3);
			if (!placedAnchor) {
				latestChunk.GetComponent<VineModelSelector>().SetAnchor(3);
				placedAnchor = true;
			}
		}
		if (newPos == Vector3.right * 2f) {
			latestChunk.GetComponent<VineModelSelector> ().SetAfter (4);
			if (!placedAnchor) {
				latestChunk.GetComponent<VineModelSelector>().SetAnchor(4);
				placedAnchor = true;
			}
		}
		if (newPos == Vector3.forward * 2f) {
			latestChunk.GetComponent<VineModelSelector> ().SetAfter (5);
			if (!placedAnchor) {
				latestChunk.GetComponent<VineModelSelector>().SetAnchor(5);
				placedAnchor = true;
			}
		}
		if (newPos == Vector3.back * 2f) {
			latestChunk.GetComponent<VineModelSelector> ().SetAfter (6);
			if (!placedAnchor) {
				latestChunk.GetComponent<VineModelSelector>().SetAnchor(6);
				placedAnchor = true;
			}
		}

	}

	void OnTriggerEnter(Collider col){
		if (col.transform.tag == "Player") {
			generating = true;
			UIprompt.gameObject.SetActive(false);
		}
	}
}
