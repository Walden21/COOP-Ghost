using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour {

	Rigidbody rb;
	public float speed = .1f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float moveH = Input.GetAxis ("Horizontal");
		float moveV = Input.GetAxis ("Vertical");

		transform.Translate (moveH * speed, 0.0f, moveV * speed);
		//transform.Translate (0.0f, 0.0f, moveV * speed);

		float camH = Input.GetAxis("Horizontal2");
		float camV = Input.GetAxis ("Vertical2");

		rb.angularVelocity = Vector3.zero;
		rb.velocity = Vector3.zero;
		transform.Rotate (0.0f, camH, 0.0f);
	}
}
