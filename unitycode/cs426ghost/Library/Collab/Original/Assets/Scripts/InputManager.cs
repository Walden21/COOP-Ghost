using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InputManager : NetworkBehaviour {

	public HumanController humanScript;
	public GhostController ghostScript;
	public int playNum;
	public Camera humanCam;
	public Camera ghostCam;
	public GameObject humanLight;
	public GameObject ghostLight;
	public CameraController humanCamScript;
	public CameraController ghostCamScript;

	// Use this for initialization
	void Start () {
		humanScript = GameObject.Find ("HumanPlayer/Human").GetComponent (typeof(HumanController)) as HumanController;
		humanCam = GameObject.FindWithTag ("humanCamera").GetComponent<Camera> ();
		humanCamScript = humanCam.GetComponent(typeof(CameraController)) as CameraController;

		ghostScript = GameObject.Find ("GhostPlayer/Ghost").GetComponent (typeof(GhostController)) as GhostController;
		ghostCam = GameObject.FindWithTag ("ghostCamera").GetComponent<Camera> ();
		ghostCamScript = ghostCam.GetComponent(typeof(CameraController)) as CameraController;

		playNum = (GameObject.Find ("GameManager").GetComponent (typeof(GameController)) as GameController).getNumber ();

		humanLight = GameObject.Find ("Lighting/HumanSkyLight");
		ghostLight = GameObject.Find ("Lighting/GhostSkyLight");

		if (isLocalPlayer) {
			if (playNum == 1) {
				humanCam.enabled = true;
				humanLight.SetActive(true);
				ghostLight.SetActive(false);
			} else {
				ghostCam.enabled = true;
				ghostLight.SetActive(true);
				humanLight.SetActive(false);
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

		bool interact = Input.GetKeyDown(KeyCode.JoystickButton0);
		if (!interact)
			interact = Input.GetKeyDown (KeyCode.Space);
		if (moveH == 0f)
			moveH = Input.GetAxis ("LjoyX");
		if (moveV == 0f)
			moveV = -1 * Input.GetAxis ("LjoyY");
		if (camH == 0f)
			camH = Input.GetAxis ("RjoyX");
		if (camV == 0f)
			camV = Input.GetAxis("RjoyY");

		CmdSendInput (moveH, moveV, camH, camV, interact, playNum);

		if (playNum == 2) {
			//ghostScript.Move (moveH, moveV, camH, interact);
			ghostCamScript.RotateCamera (camH, camV);
		}


	}

	[Command]
	void CmdSendInput(float moveH, float moveV, float camH, float camV, bool interact, int pNum){
		if (pNum == 1) {
			humanScript.Move (moveH, moveV, camH, interact);
			humanCamScript.RotateCamera (camH, camV);
		} 
		else if (pNum == 2) {
			ghostScript.Move (moveH, moveV, camH, interact);
			//ghostCamScript.RotateCamera (camH, camV);
		}
	}
}
