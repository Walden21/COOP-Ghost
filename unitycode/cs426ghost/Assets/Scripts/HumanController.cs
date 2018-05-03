using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HumanController : NetworkBehaviour
{
    public Rigidbody rb;
    public float speed = .3f;
    public bool interacting;

	public GameObject hand;
	public GameObject heldObject;
	public bool holding = false;

    public GameObject interactCollider;
    public GameObject model;
    public Animator anim;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = model.GetComponent<Animator>();
        interacting = false;
        interactCollider.SetActive(false);
    }


	public void Move(float hor, float vert, float camH, bool interact, bool drop)
    {
        interacting = interact;
        if (interact)
        {

				interactCollider.SetActive (true);
        }
        else
        {
            interactCollider.SetActive(false);
        }

		if (drop) {
			if (holding) {
				heldObject.GetComponent<interactableController>().dropObject ();
				holding = false;
			} 
		}

        this.transform.Translate(hor * speed, 0.0f, vert * speed);
        this.transform.Rotate(0.0f, camH, 0.0f);

		if (holding) {
			heldObject.transform.position = new Vector3(hand.transform.position.x, hand.transform.position.y, hand.transform.position.z);
			heldObject.transform.rotation = hand.transform.rotation;
			heldObject.transform.Rotate(new Vector3 (0f,-90f,0f));
		}

        anim.SetFloat("ForwardSpeed", vert);
        anim.SetBool("test", true);
        //ebug.Log (vert);
    }
		
}
