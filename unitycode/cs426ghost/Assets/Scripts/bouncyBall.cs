using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncyBall : interactableController {

	Rigidbody rb;

	public override void Setup(){
		rb = GetComponent<Rigidbody> ();
	}

	public override void humanInteraction(){
		Vector3 dir =  this.transform.position - human.transform.position;
		rb.AddForce (dir * 1000);
	}

	public override void ghostInteraction(){
		Vector3 dir =  this.transform.position - ghost.transform.position;
		rb.AddForce (dir * 1000);
	}
}
