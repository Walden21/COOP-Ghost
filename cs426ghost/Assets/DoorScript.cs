using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

	float openAngle = 0f;
	float closeAngle = 90f;
	bool open;

	//float timeSinceOpen = 0f;
	//float delayTimer = 2000f;

	// Use this for initialization
	void Start () {
		open = false;
	}

	void OnTriggerStay (Collider col){
		if (col.gameObject.tag == "human") {
			open = true;
		}
	}

	void OnTriggerExit (Collider col){
		if (col.gameObject.tag == "human") {
			open = false;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (open == true) {
			var tar = Quaternion.Euler (0, openAngle, 0);
			transform.localRotation = Quaternion.Slerp (transform.localRotation, tar, Time.deltaTime);
		} 
		/*
		else if (open == false) {
			var tar = Quaternion.Euler (0, closeAngle, 0);
			transform.localRotation = Quaternion.Slerp (transform.localRotation, tar, Time.deltaTime);
		}
		*/
	}
}
