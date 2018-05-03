using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueboard : MonoBehaviour {
    public Transform piccam;
    Quaternion originalrotation;
	// Use this for initialization
	void Start () {
        originalrotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = piccam.rotation * originalrotation;

    }
}
