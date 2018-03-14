using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class playerControls : NetworkBehaviour {

	public HumanController humanScript;
	public GhostController ghostScript;
	public int playNum;
	public Camera humanCam;
	public Camera ghostCam;

	// Use this for initialization
	void Start () {
		humanScript = GameObject.Find ("HumanPlayer/Human").GetComponent (typeof(HumanController)) as HumanController;
		ghostScript = GameObject.Find ("GhostPlayer/Ghost").GetComponent (typeof(GhostController)) as GhostController;

		humanCam = GameObject.FindWithTag ("humanCamera").GetComponent<Camera> ();
		ghostCam = GameObject.FindWithTag ("ghostCamera").GetComponent<Camera> ();

		playNum = (GameObject.Find ("GameManager").GetComponent (typeof(gameScript)) as gameScript).getNumber ();
		if (isLocalPlayer) {
			if (playNum == 1) {
				humanCam.enabled = true;
				ghostCam.enabled = false;
			} else {
				//humanCam.enabled = false;
				ghostCam.enabled = true;
			}
		}

	}

	void FixedUpdate () {
		
		if (!isLocalPlayer) {
			return;
		}
		
		float moveH = Input.GetAxis("Horizontal");
		float moveV = Input.GetAxis("Vertical");

		float camH = Input.GetAxis("Horizontal2");
		float camV = Input.GetAxis("Vertical2");

		if (playNum == 1) {
			humanScript.move (moveH, moveV, camH, camV);
		} else if (playNum == 2) {
			ghostScript.move (moveH, moveV, camH, camV);
		}
	}
}
