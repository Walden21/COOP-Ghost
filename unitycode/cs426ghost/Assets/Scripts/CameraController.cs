using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform player;

	public LayerMask colLayers; 
	//^ WILL NEED FOR GHOST CAMERA NOT COLLIDING WITH WALLS
	//^ MIGHT ALSO NEED FOR PLAYER CAMERA NOT COLLIDING WITH GHOSTS

	Vector3 offX;
	Vector3 offY;
	Vector3 yPos;

	float timeOfCol;

	public bool smooth = false;

	float dist;

	// Use this for initialization
	void Start () {
		timeOfCol = Time.time;
		dist = 15f;
		offX = new Vector3(0f, 0f, -dist);
		offY = new Vector3 (0f, 3f, 0f);
		yPos = new Vector3 (0f, 3f, 0f);
	}

	public void RotateCamera(float camH, float camV){

		offX = Quaternion.AngleAxis (camH, Vector3.up) * offX;
		offY += new Vector3 (0f, camV/5f, 0f);

		RaycastHit col;
		if (Physics.Linecast (player.position + yPos, (player.position + offX + offY), out col, colLayers.value)) {
			Vector3 dir = player.position + yPos - col.point;

			if (smooth) {
				transform.position = Vector3.Lerp (transform.position, col.point + (dir * .15f) + yPos, 5f * Time.deltaTime);
				timeOfCol = Time.time;
			}
			else
				transform.position = col.point + (dir * .15f) + yPos;
		} 
		else {
			if (smooth)
				transform.position = Vector3.Lerp (transform.position, player.position + offX + offY + yPos, 10f * (Time.time - timeOfCol+1) * Time.deltaTime);
			else
				transform.position = player.position + offX + offY + yPos;
		}
		transform.LookAt (player.position + yPos);
	}

	// Update is called once per frame
	void LateUpdate () {

	}
}
