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

	float dist;

	// Use this for initialization
	void Start () {
		dist = 15f;
		offX = new Vector3(0f, 0f, -dist);
		offY = Vector3.zero;
	}

	public void RotateCamera(float camH, float camV){

		offX = Quaternion.AngleAxis (camH, Vector3.up) * offX;
		offY += new Vector3 (0f, camV/5f, 0f);

		RaycastHit col;
		if (Physics.Linecast (player.position, (player.position + offX + offY), out col, colLayers.value)) {
			Vector3 dir = player.position - col.point;
			transform.position = col.point + (dir *.1f);
		} else {
			transform.position = player.position + offX + offY;
		}
		transform.LookAt (player.position);
	}

	// Update is called once per frame
	void LateUpdate () {

	}
}
