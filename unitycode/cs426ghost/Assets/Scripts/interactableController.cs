using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class interactableController : NetworkBehaviour {

	public GameObject human;
	public GameObject ghost;

	public HumanController humanScript;
	public GhostController ghostScript;

	// Use this for initialization
	void Start () {
		humanScript = human.GetComponent (typeof(HumanController)) as HumanController;
		ghostScript = ghost.GetComponent (typeof(GhostController)) as GhostController;

		Setup ();
	}
		
	public virtual void Setup(){

	}

	public virtual void humanInteraction(){
		
	}

	public virtual void ghostInteraction(){

	}

	public virtual void dropObject(){

	}

	public virtual void deleteObject(){

	}

	public virtual void otherInteraction (Collider col){
	
	}

	void OnTriggerEnter (Collider col){
		if (col.gameObject.name == "HumanCollider") {
			humanInteraction ();
		} else if (col.gameObject.name == "GhostCollider") {
			ghostInteraction ();
		}
		else{
			otherInteraction(col);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
