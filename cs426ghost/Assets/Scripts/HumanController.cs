using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HumanController : MonoBehaviour
{
	//Rigidbody rb;
	public float speed = .1f;

	// Use this for initialization
	void Start () {
		//rb = GetComponent<Rigidbody> ();
	}

	public void move(float hor, float vert, float camH, float camV){
		transform.Translate(hor * speed, 0.0f, vert * speed);
		transform.Rotate(0.0f, camH, 0.0f);
	}

	// Update is called once per frame
	void FixedUpdate () {

	}
}
