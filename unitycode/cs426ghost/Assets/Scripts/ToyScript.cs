using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyScript : interactableController
{
	Rigidbody rb;
	public GameObject hand;
	public GameObject throwSpot;
	public GameObject dog;

	public override void Setup()
	{
		rb = GetComponent<Rigidbody>();
	}

	public override void humanInteraction()
	{
		humanScript.heldObject = this.gameObject;
		humanScript.holding = true;
		rb.detectCollisions = false;
		rb.isKinematic = true;
	}

	public override void ghostInteraction()
	{
		Vector3 dir = this.transform.position - ghost.transform.position;
		rb.AddForce(dir * 1000);
	}

	//TODO: This needs to get called by human script.
	public override void dropObject(){
		this.transform.position = throwSpot.transform.position; 
		rb.detectCollisions = true;
		rb.isKinematic = false;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.AddForce (human.transform.forward * 500f);
	}

	//TODO: should be called by human script
	public override void deleteObject(){
		Destroy(gameObject);
		Destroy(this);
	}

	public override void otherInteraction(Collider col){
		if (col.gameObject.name == "GrowlTrigger") {
			DogPathing dogScript = dog.GetComponent<DogPathing> ();
			dogScript.catchTreat ();
			deleteObject ();
		}
	}
		
	public void Update()
	{
	}
}
