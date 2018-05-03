using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ComputerScript : interactableController {

	public GameObject dog;
	public DogPathing dogScript;

	public GameObject tObj;
	Text t;
	int frameCounter = 0;

	bool isActive = false;

	// Use this for initialization
	public override void Setup() {
		CmdSetup ();
		CmdChangeText("Login\nUsername: Frank\nPassword: _____", 74);
		CmdNotActive ();
	}

	[Command]
	void CmdSetup(){
		t = tObj.GetComponent<Text> ();
		dogScript = dog.GetComponent<DogPathing> ();
		RpcSetup ();
	}

	[ClientRpc]
	public void RpcSetup(){
		if (!isLocalPlayer) {
			t = tObj.GetComponent<Text> ();
			dogScript = dog.GetComponent<DogPathing> ();
		}
	}

	public override void ghostInteraction()
	{
		if (isActive) {
			isActive = false;
			CmdNotActive ();
		} else {
			isActive = true;
			CmdActive ();
		}
	}

	[Command]
	void CmdActive(){
		tObj.SetActive (true);
		RpcActive();
	}

	[ClientRpc]
	public void RpcActive(){
		if (!isLocalPlayer) {
			tObj.SetActive (true);
		}
	}

	[Command]
	void CmdNotActive(){
		tObj.SetActive (false);
		RpcNotActive();
	}

	[ClientRpc]
	public void RpcNotActive(){
		if (!isLocalPlayer) {
			tObj.SetActive (false);
		}
	}

	[Command]
	void CmdChangeText(string s, int size){
		t.text = s;
		t.fontSize = size;
		RpcChangeText(s, size);
	}

	[ClientRpc]
	public void RpcChangeText(string s, int size){
		if (!isLocalPlayer) {
			t = tObj.GetComponent<Text> ();
			t.text = s;
			t.fontSize = size;
		}
	}

	public override void humanInteraction(){
		if (!isActive) {
			isActive = true;
			CmdActive ();
		} 
		else if (frameCounter >= 0 && frameCounter <= 120) {
			if (dogScript.hasToy) {
				correctPasswordEntered ();
			} 
			else {
				CmdChangeText("PASSWORD INCORRECT", 100);
				frameCounter = -180;
			}
		}
	}

	//TODO: ADD 'CUTSCENE' HERE (PROBABLY?)
	public void correctPasswordEntered(){
		CmdChangeText("CORRECT PASSWORD", 100);
		frameCounter = 999;
	}

	// Update is called once per frame
	void Update () {
		if (isActive) {
			frameCounter++;
			if (frameCounter == 60) {
				CmdChangeText("Login\nUsername: Frank\nPassword: _____", 74);
			} else if (frameCounter == 120) {
				CmdChangeText("Login\nUsername: Frank\nPassword: |____ ", 74);
				frameCounter = 0;
			}
		}
	}
}
