using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GhostController : NetworkBehaviour
{
    Rigidbody rb;
    public float speed = .1f;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

	[Command]
	public void CmdMove(float hor, float vert, float camH){
		rb.transform.Translate(hor * speed, 0.0f, vert * speed);
		rb.transform.Rotate(0.0f, camH, 0.0f);
	}

	public void Move(float hor, float vert, float camH){
		rb.transform.Translate(hor * speed, 0.0f, vert * speed);
		rb.transform.Rotate(0.0f, camH, 0.0f);
	}

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
