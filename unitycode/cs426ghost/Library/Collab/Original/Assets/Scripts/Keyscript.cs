using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyscript : interactableController
{
    public int timerval;
    Rigidbody rb;
    GameObject hand;
    int timer;

    public override void Setup()
    {
        rb = GetComponent<Rigidbody>();
        hand = GameObject.Find("Character1_Ctrl_RightHandIndexEffector");
    }

    public override void humanInteraction()
    {
        Debug.Log("human interacted with key");
        timer = timerval;

        humanScript.holdingkey = !humanScript.holdingkey;
        if (!humanScript.holdingkey) Debug.Log("human dropped key");
    }

    public override void ghostInteraction()
    {
        Vector3 dir = this.transform.position - ghost.transform.position;
        rb.AddForce(dir * 1000);
    }

    public void Update()
    {
        if(humanScript.holdingkey)
        {
            rb.useGravity = false;
            this.transform.position = new Vector3(hand.transform.position.x, hand.transform.position.y, hand.transform.position.z);
            if(timer > 0) timer--;
            if(timer <= 0)
            {
                humanScript.holdingkey = false;
            }
            return;
        }
        

        if (humanScript.deletekey)
        {
            Destroy(gameObject);
            Destroy(rb);
            Destroy(this);
        }

        rb.useGravity = true;      
    }
}
