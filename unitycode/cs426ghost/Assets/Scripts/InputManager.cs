using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InputManager : NetworkBehaviour {

	public HumanController humanScript;
	public GhostController ghostScript;
	public int playNum;
	public Camera humanCam;
	public bool humanUI;
	public bool ghostUI;
	public GameObject humanMiniMap;
	public Camera ghostCam;
	public GameObject ghostMiniMap;
	public GameObject humanLight;
	public GameObject ghostLight;
	public CameraController humanCamScript;
	public CameraController ghostCamScript;

	public GameObject controlsImage;
	public bool controlsBool;

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

		ghostUI = true;
		humanUI = true;

		humanMiniMap = GameObject.Find ("MiniMapCanvas/MapImages/HumanMiniMap");
		ghostMiniMap = GameObject.Find ("MiniMapCanvas/MapImages/GhostMiniMap");

		controlsImage = GameObject.Find ("MiniMapCanvas/MapImages/ControlsImage");
		controlsBool = false;

		if (isLocalPlayer) {
			if (playNum == 1) {
				humanCam.enabled = true;
				GameObject.Find ("humanCamera").GetComponent<AudioListener> ().enabled = true;
				humanMiniMap.SetActive (true);
				humanLight.SetActive(true);
				ghostLight.SetActive(false);
			} else {
				ghostCam.enabled = true;
				GameObject.Find ("ghostCamera").GetComponent<AudioListener> ().enabled = true;
				ghostMiniMap.SetActive (true);
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

		//bool squre = Input.GetKeyDown(KeyCode.JoystickButton0); //square
		bool interact = Input.GetKeyDown(KeyCode.JoystickButton1); //x
		bool drop = Input.GetKeyDown(KeyCode.JoystickButton2); //circle
		bool uiDisplay = Input.GetKeyDown(KeyCode.JoystickButton3); //triangle

		bool controls = Input.GetKeyDown (KeyCode.JoystickButton9);

		if (!drop)
			drop = Input.GetKeyDown (KeyCode.Q);
		if (!interact)
			interact = Input.GetKeyDown (KeyCode.Space);
		if (!uiDisplay)
			uiDisplay = Input.GetKeyDown (KeyCode.M);		
		if (moveH == 0f)
			moveH = Input.GetAxis ("LjoyX");
		if (moveV == 0f)
			moveV = -1 * Input.GetAxis ("LjoyY");
		if (camH == 0f)
			camH = Input.GetAxis ("RjoyX");
		if (camV == 0f)
			camV = Input.GetAxis("RjoyY");

		CmdSendInput (moveH, moveV, camH, camV, interact, drop, playNum);

		if (controls) {
			if (controlsBool) {
				controlsImage.SetActive (false);
				controlsBool = !controlsBool;
			} 
			else {
				controlsImage.SetActive (true);
				controlsBool = !controlsBool;
			}
		}

		if (playNum == 2) {
			//ghostScript.Move (moveH, moveV, camH, interact);
			ghostCamScript.RotateCamera (camH, camV);
			if (uiDisplay) {
				if (ghostUI) {
					ghostUI = !ghostUI;
					ghostMiniMap.SetActive (false);
				} 
				else {
					ghostUI = !ghostUI;
					ghostMiniMap.SetActive (true);
				}
			}
		} 
		else {
			if (uiDisplay) {
				if (humanUI) {
					humanUI = !humanUI;
					humanMiniMap.SetActive (false);
				} 
				else {
					humanUI = !humanUI;
					humanMiniMap.SetActive (true);
				}
			}
		}

	}

	[Command]
	void CmdSendInput(float moveH, float moveV, float camH, float camV, bool interact, bool drop, int pNum){
		if (pNum == 1) {
			humanScript.Move (moveH, moveV, camH, interact, drop);
			humanCamScript.RotateCamera (camH, camV);
		} 
		else if (pNum == 2) {
			ghostScript.Move (moveH, moveV, camH, interact);
			//ghostCamScript.RotateCamera (camH, camV);
		}
	}
}
