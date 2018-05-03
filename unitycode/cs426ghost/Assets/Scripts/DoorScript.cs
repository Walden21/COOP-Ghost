using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DoorScript : interactableController
{
    public AudioClip doorsound;
    AudioSource source;

    public bool islocked = false;
    float openAngle = 90f;
    float closeAngle = 0f;
    public bool opened = false;
    public bool flipDirection = false;

    public override void Setup()
    {
		source = this.GetComponent<AudioSource> ();
		source.clip = doorsound;
    }

    public override void humanInteraction()
    {
        if (!islocked)
        {
			source.Play();
            opened = !opened;
        }
        else
        {
			if (humanScript.holding && humanScript.heldObject.name == "key")
            {
                islocked = false;
				humanScript.holding = false;
				humanScript.heldObject.GetComponent<interactableController>().deleteObject();
            }
        }
    }

    public override void ghostInteraction()
	{
		
    }

	[Command]
	void CmdOpen(Quaternion tar){
		transform.localRotation = Quaternion.Slerp(transform.localRotation, tar, Time.deltaTime);
		RpcOpen (tar);
	}

	[ClientRpc]
	void RpcOpen(Quaternion tar){
		transform.localRotation = Quaternion.Slerp(transform.localRotation, tar, Time.deltaTime);
	}

    void Update()
    {
        if (opened == true)
        {
            Quaternion tar;
            if (flipDirection) 
				tar = Quaternion.Euler(0, -openAngle, 0);
            else 
				tar = Quaternion.Euler(0, openAngle, 0);
			CmdOpen (tar);
        }
        else if (opened == false)
        {
			Quaternion tar = Quaternion.Euler(0, closeAngle, 0);
			CmdOpen (tar);
        }
    }
}