using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    Rigidbody rb;
    public float speed = .25f;
	public bool interacting;

	public GameObject interactCollider;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
		interacting = false;
		interactCollider.SetActive (false);
    }
		
	public void Move(float hor, float vert, float camH, bool interact){
		interacting = interact;
		if (interact) {
			interactCollider.SetActive (true);
		} else {
			interactCollider.SetActive (false);
		}

		this.transform.Translate(hor * speed, 0.0f, vert * speed);
		this.transform.Rotate(0.0f, camH, 0.0f);

		//rb.transform.Translate(hor * speed, 0.0f, vert * speed);
		//rb.transform.Rotate(0.0f, camH, 0.0f);
	}

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
