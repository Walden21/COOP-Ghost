using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMenus : MonoBehaviour {

	public int menuNum = 0;

	TitleScript title;

	// Use this for initialization
	void Start () {
		title = this.GetComponent (typeof(TitleScript)) as TitleScript;
	}
	
	// Update is called once per frame
	void Update () {
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


		if (menuNum == 0) {
			if (interact) {
				title.StartGame ();
			}
		}
		if (menuNum == 1) {
			if (interact) {
				title.StartHouseScene ();
			}
		}
	}
}
