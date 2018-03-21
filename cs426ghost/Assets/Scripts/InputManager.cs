using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InputManager : NetworkBehaviour {

	public HumanController humanScript;
	public GhostController ghostScript;
	public int playNum;
	public Camera cam;
	public CameraController camScript;

	// Use this for initialization
	void Start () {
		humanScript = GameObject.Find ("HumanPlayer/Human").GetComponent (typeof(HumanController)) as HumanController;
		ghostScript = GameObject.Find ("GhostPlayer/Ghost").GetComponent (typeof(GhostController)) as GhostController;

		playNum = (GameObject.Find ("GameManager").GetComponent (typeof(GameController)) as GameController).getNumber ();
		if (isLocalPlayer) {
			if (playNum == 1) {
				cam = GameObject.FindWithTag ("humanCamera").GetComponent<Camera> ();
				cam.enabled = true;
				camScript = cam.GetComponent(typeof(CameraController)) as CameraController;
			} else {
				//humanCam.enabled = false;
				cam = GameObject.FindWithTag ("ghostCamera").GetComponent<Camera> ();
				cam.enabled = true;
				camScript = cam.GetComponent(typeof(CameraController)) as CameraController;
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
			humanScript.Move (moveH, moveV, camH);
			camScript.RotateCamera (camH, camV);

		} else if (playNum == 2) {
			ghostScript.Move (moveH, moveV, camH);
			ghostScript.CmdMove (moveH, moveV, camH);
			camScript.RotateCamera (camH, camV);
		}
	}
}
