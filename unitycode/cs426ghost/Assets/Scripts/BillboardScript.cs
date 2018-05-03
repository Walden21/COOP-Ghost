using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour {

	public GameObject human;
	public GameObject ghost;

	public bool humanIfTrue = true;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (humanIfTrue)
			transform.LookAt (transform.position + human.transform.rotation * Vector3.forward, human.transform.rotation * Vector3.up);
		else
			transform.LookAt (transform.position + ghost.transform.rotation * Vector3.forward, ghost.transform.rotation * Vector3.up);
		transform.Rotate (new Vector3(-90, 0,0));
	}
}
