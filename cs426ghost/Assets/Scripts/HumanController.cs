using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HumanController : MonoBehaviour
{
	//public Rigidbody rb;
	public float speed = .1f;

	// Use this for initialization
	void Start () {
		//rb = GetComponent<Rigidbody> ();
	}

	public void Move(float hor, float vert, float camH){
		this.transform.Translate(hor * speed, 0.0f, vert * speed);
		this.transform.Rotate(0.0f, camH, 0.0f);
	}

	// Update is called once per frame
	void FixedUpdate () {

	}
}
